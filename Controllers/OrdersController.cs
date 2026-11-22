using System.Security.Claims;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using PopPop.ExternalServices.Pay;
using PopPop.Models;
using PopPop.Services.Interfaces;

namespace PopPop.Controllers;

[Authorize]
public class OrdersController : Controller
{
    private readonly ICartService _cartService;
    private readonly IOrderService _orderService;
    private readonly IVnPayClient _vnPayClient;
    private readonly VnPayOptions _vnPayOptions;
    private readonly IWebHostEnvironment _environment;

    public OrdersController(
        ICartService cartService,
        IOrderService orderService,
        IVnPayClient vnPayClient,
        IOptions<VnPayOptions> vnPayOptions,
        IWebHostEnvironment environment)
    {
        _cartService = cartService;
        _orderService = orderService;
        _vnPayClient = vnPayClient;
        _vnPayOptions = vnPayOptions.Value;
        _environment = environment;
    }

    [HttpGet]
    public async Task<IActionResult> Index(CancellationToken cancellationToken)
    {
        var orders = await _orderService.GetOrdersForUserAsync(GetUserId(), cancellationToken);
        return View(orders);
    }

    [HttpGet]
    public async Task<IActionResult> Checkout(CancellationToken cancellationToken)
    {
        var model = await CreateCheckoutViewModelAsync(cancellationToken);
        if (model.Items.Count == 0 || !model.Items.All(item => item.StockQuantity >= item.Quantity))
        {
            TempData["CartError"] = model.Items.Count == 0
                ? "Giỏ hàng đang trống."
                : "Tồn kho đã thay đổi. Vui lòng kiểm tra lại giỏ hàng trước khi đặt hàng.";
            return RedirectToAction("Index", "Cart");
        }

        return View(model);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Checkout(CheckoutViewModel model, CancellationToken cancellationToken)
    {
        if (!ModelState.IsValid)
        {
            await LoadCartSummaryAsync(model, cancellationToken);
            model.IsVnPayConfigured = CanUseVnPay;
            return View(model);
        }

        try
        {
            var result = await _orderService.PlaceOrderAsync(
                GetUserId(),
                model,
                model.PaymentMethod,
                HttpContext.Connection.RemoteIpAddress?.ToString() ?? "127.0.0.1",
                GetCallbackBaseUrl(),
                cancellationToken);
            return result.PaymentUrl is not null
                ? Redirect(result.PaymentUrl)
                : RedirectToAction(nameof(Details), new { id = result.OrderId });
        }
        catch (InvalidOperationException exception)
        {
            ModelState.AddModelError(string.Empty, exception.Message);
            await LoadCartSummaryAsync(model, cancellationToken);
            model.IsVnPayConfigured = CanUseVnPay;
            return View(model);
        }
    }

    [AllowAnonymous]
    [HttpGet]
    public async Task<IActionResult> VnPayIpn(CancellationToken cancellationToken)
    {
        var response = await _orderService.ProcessVnPayPaymentAsync(GetPaymentParameters(), cancellationToken);
        return new JsonResult(response);
    }

    [AllowAnonymous]
    [HttpGet]
    public async Task<IActionResult> VnPayReturn(CancellationToken cancellationToken)
    {
        var parameters = GetPaymentParameters();
        var signatureValid = _vnPayClient.ValidateSignature(parameters);
        var isLocalDevelopmentCallback = _environment.IsDevelopment()
            && Request.IsHttps
            && Request.Host.HasValue
            && Uri.TryCreate($"{Request.Scheme}://{Request.Host}", UriKind.Absolute, out var requestUri)
            && requestUri.IsLoopback;
        var paymentResponse = signatureValid && isLocalDevelopmentCallback
            ? await _orderService.ProcessVnPayPaymentAsync(parameters, cancellationToken)
            : null;
        var returnProcessed = paymentResponse?.ResponseCode == "00"
            || (paymentResponse?.ResponseCode == "02" && paymentResponse.Message == "Order already confirmed");
        var hasSuccessfulTransaction = parameters.TryGetValue("vnp_ResponseCode", out var responseCode)
            && responseCode == "00"
            && parameters.TryGetValue("vnp_TransactionStatus", out var transactionStatus)
            && transactionStatus == "00";
        var transactionSucceeded = signatureValid
            && (!isLocalDevelopmentCallback || returnProcessed)
            && hasSuccessfulTransaction;

        var model = new VnPayReturnViewModel
        {
            OrderReference = parameters.GetValueOrDefault("vnp_TxnRef") ?? string.Empty,
            IsSignatureValid = signatureValid,
            IsTransactionSuccessful = transactionSucceeded,
            IsLocalCallback = isLocalDevelopmentCallback,
            IsReturnProcessed = returnProcessed,
            Message = !signatureValid
                ? "Không thể xác thực phản hồi thanh toán. Vui lòng kiểm tra đơn hàng trong tài khoản."
                : isLocalDevelopmentCallback && !returnProcessed
                    ? "Không thể cập nhật đơn hàng từ kết quả VNPay. Vui lòng kiểm tra mã đơn hàng và số tiền thanh toán."
                : transactionSucceeded
                    ? isLocalDevelopmentCallback
                        ? "Thanh toán VNPay Sandbox thành công và trạng thái đơn hàng đã được cập nhật."
                        : "VNPay đã chuyển kết quả về website. Đơn hàng chỉ được xác nhận sau khi hệ thống nhận và xác thực thông báo IPN."
                    : isLocalDevelopmentCallback
                        ? "Giao dịch không thành công. Trạng thái đơn hàng đã được cập nhật và sản phẩm được hoàn lại vào giỏ hàng."
                    : "Giao dịch chưa thành công. Nếu giỏ hàng được khôi phục, bạn có thể thử đặt hàng lại."
        };

        return View("PaymentResult", model);
    }

    [HttpGet]
    public async Task<IActionResult> Details(int id, CancellationToken cancellationToken)
    {
        var order = await _orderService.GetDetailsForUserAsync(id, GetUserId(), cancellationToken);
        return order is null ? NotFound() : View(order);
    }

    private async Task<CheckoutViewModel> CreateCheckoutViewModelAsync(CancellationToken cancellationToken)
    {
        var model = new CheckoutViewModel
        {
            IsVnPayConfigured = CanUseVnPay
        };
        await LoadCartSummaryAsync(model, cancellationToken);
        return model;
    }

    private bool CanUseVnPay => _orderService.IsVnPayConfigured && GetCallbackBaseUrl() is not null;

    private string? GetCallbackBaseUrl()
    {
        var callbackBaseUrl = _vnPayOptions.CallbackBaseUrl;
        if (string.IsNullOrWhiteSpace(callbackBaseUrl))
        {
            if (!Request.IsHttps || !Request.Host.HasValue)
            {
                return null;
            }

            callbackBaseUrl = $"{Request.Scheme}://{Request.Host}{Request.PathBase}";
        }

        if (!Uri.TryCreate(callbackBaseUrl, UriKind.Absolute, out var callbackUri)
            || callbackUri.Scheme != Uri.UriSchemeHttps
            || (callbackUri.IsLoopback && !_environment.IsDevelopment())
            || !string.IsNullOrEmpty(callbackUri.UserInfo)
            || !string.IsNullOrEmpty(callbackUri.Query)
            || !string.IsNullOrEmpty(callbackUri.Fragment))
        {
            return null;
        }

        return callbackUri.GetLeftPart(UriPartial.Authority) + callbackUri.AbsolutePath.TrimEnd('/');
    }

    private Dictionary<string, string> GetPaymentParameters()
    {
        return Request.Query.ToDictionary(
            parameter => parameter.Key,
            parameter => parameter.Value.ToString(),
            StringComparer.Ordinal);
    }

    private async Task LoadCartSummaryAsync(CheckoutViewModel model, CancellationToken cancellationToken)
    {
        var cart = await _cartService.GetCartAsync(GetUserId(), cancellationToken);
        model.Items = cart.Items;
        model.ShippingFee = 0m;
    }

    private string GetUserId()
    {
        return User.FindFirstValue(ClaimTypes.NameIdentifier)
            ?? throw new InvalidOperationException("Không xác định được tài khoản hiện tại.");
    }
}

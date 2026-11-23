using System.ComponentModel.DataAnnotations;
using PopPop.Data.Entities.Enums;

namespace PopPop.Models;

public sealed class CheckoutViewModel
{
    [Required(ErrorMessage = "Vui lòng nhập tên người nhận.")]
    [StringLength(256)]
    [Display(Name = "Tên người nhận")]
    public string RecipientName { get; set; } = string.Empty;

    [Required(ErrorMessage = "Vui lòng nhập số điện thoại.")]
    [Phone(ErrorMessage = "Số điện thoại không hợp lệ.")]
    [StringLength(20)]
    [Display(Name = "Số điện thoại")]
    public string RecipientPhone { get; set; } = string.Empty;

    [Required(ErrorMessage = "Vui lòng nhập địa chỉ giao hàng.")]
    [StringLength(500)]
    [Display(Name = "Địa chỉ giao hàng")]
    public string ShippingAddress { get; set; } = string.Empty;

    [StringLength(1000)]
    [Display(Name = "Ghi chú")]
    public string? Note { get; set; }

    [StringLength(50)]
    [Display(Name = "Mã voucher")]
    public string? VoucherCode { get; set; }

    public PaymentMethod PaymentMethod { get; set; } = PaymentMethod.CashOnDelivery;

    public bool IsVnPayConfigured { get; set; }
    public decimal EstimatedDiscountAmount { get; set; }
    public string? VoucherMessage { get; set; }
    public bool IsVoucherValid { get; set; }

    public IReadOnlyList<CartLineViewModel> Items { get; set; } = [];
    public decimal Subtotal => Items.Sum(item => item.LineTotal);
    public decimal ShippingFee { get; set; }
    public decimal TotalAmount => Math.Max(0m, Subtotal - EstimatedDiscountAmount) + ShippingFee;
}

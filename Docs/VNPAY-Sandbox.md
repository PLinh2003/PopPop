# VNPay Sandbox setup

The checkout keeps COD available when VNPay is not configured. VNPay uses the sandbox payment URL by default; no live credentials are included in source control.

## 1. Get Sandbox merchant information

Register for a VNPay test merchant account and obtain its `TmnCode` and `HashSecret` from VNPay. Do not send the `HashSecret` in chat, commit it, or put it in `appsettings.json`.

## 2. Configure local secrets

From PowerShell, run these commands from the repository directory, replacing the placeholders with your Sandbox values:

```powershell
dotnet user-secrets set "Payment:VnPay:TmnCode" "<SANDBOX_TMN_CODE>" --project .\PopPop.csproj
dotnet user-secrets set "Payment:VnPay:HashSecret" "<SANDBOX_HASH_SECRET>" --project .\PopPop.csproj
```

The app derives the browser return URL as `/Orders/VnPayReturn` from the HTTPS host used to open checkout. `CallbackBaseUrl` is an optional User Secrets override for deployments where the public origin cannot be inferred correctly.

For local testing, no public tunnel, port forwarding, or IPN URL registration is needed. Trust the local HTTPS development certificate and run the HTTPS profile:

```powershell
dotnet dev-certs https --trust
dotnet run --launch-profile https
```

Open the site at `https://localhost:7253` (or the HTTPS URL shown by Visual Studio), then checkout through the VNPay Sandbox page. VNPay redirects the browser back to localhost; in Development, the app verifies the signed return and checks the order reference, amount, and transaction status to update the order. The Sandbox payment page still opens in the browser, but the app does not need to be reachable from the public internet.

For a deployed site, use its public HTTPS domain and configure the Sandbox merchant IPN URL as `https://<your-public-https-host>/Orders/VnPayIpn`. Production order status is confirmed by the authenticated IPN, not by the browser return.

The Sandbox payment URL defaults to `https://sandbox.vnpayment.vn/paymentv2/vpcpay.html`. Override `Payment:VnPay:PaymentUrl` through User Secrets only if VNPay provides a different Sandbox URL.

## 3. Apply the database migration

```powershell
dotnet ef database update --project .\PopPop.csproj
```

## 4. Run and test

Use only the test cards and instructions shown in the VNPay Sandbox portal; do not use real card data. For deployments behind a reverse proxy, configure that proxy as trusted before relying on forwarded host/protocol headers.

Local Development processes a valid signed browser return because VNPay cannot send IPN notifications to localhost. Outside Development, browser returns are display-only and IPN remains authoritative. Pending VNPay orders expire after 15 minutes; failed or expired orders release reserved stock and restore their items to the customer's cart.

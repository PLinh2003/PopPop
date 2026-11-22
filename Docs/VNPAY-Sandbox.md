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

VNPay must be able to reach the application. For local testing, run the app and expose its HTTP port through a public HTTPS tunnel, for example:

```powershell
ngrok http http://localhost:5263
```

Open the site through the tunnel's HTTPS host (not `localhost`) and configure the Sandbox merchant IPN URL as `https://<your-public-https-host>/Orders/VnPayIpn`. If the tunnel URL changes, update the IPN URL in the merchant portal. For a deployed site, use its public HTTPS domain instead.

The Sandbox payment URL defaults to `https://sandbox.vnpayment.vn/paymentv2/vpcpay.html`. Override `Payment:VnPay:PaymentUrl` through User Secrets only if VNPay provides a different Sandbox URL.

## 3. Apply the database migration

```powershell
dotnet ef database update --project .\PopPop.csproj
```

## 4. Run and test

Start the app, open it through its public HTTPS URL, and use the test cards and instructions shown in the VNPay Sandbox portal; do not use real card data. ASP.NET Core accepts forwarded host/protocol headers only from its default trusted proxies; if the deployment uses a different reverse proxy, configure that proxy as trusted before relying on forwarded headers.

A browser return is only a display result. The order is marked paid only after the app verifies VNPay's signed IPN and checks the order reference, amount, and transaction status. Pending VNPay orders expire after 15 minutes; failed or expired orders release reserved stock and restore their items to the customer's cart.

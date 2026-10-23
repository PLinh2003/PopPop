using System.Net;

namespace PopPop.ExternalServices
{
    internal static class EmailTemplate
    {
        public static string CreateHtml(string subject, string message)
        {
            var safeSubject = WebUtility.HtmlEncode(subject);

            return $"""
                <!doctype html>
                <html lang="vi">
                <head>
                    <meta charset="utf-8">
                    <meta name="viewport" content="width=device-width, initial-scale=1">
                    <title>{safeSubject}</title>
                </head>
                <body style="margin:0;padding:0;background-color:#f3f5f9;font-family:Arial,Helvetica,sans-serif;color:#263247;">
                    <table role="presentation" width="100%" cellspacing="0" cellpadding="0" border="0" style="background-color:#f3f5f9;padding:32px 12px;">
                        <tr>
                            <td align="center">
                                <table role="presentation" width="600" cellspacing="0" cellpadding="0" border="0" style="width:100%;max-width:600px;background-color:#ffffff;border-radius:12px;overflow:hidden;">
                                    <tr>
                                        <td style="background-color:#172554;padding:24px 32px;color:#ffffff;">
                                            <div style="font-size:24px;font-weight:700;letter-spacing:0.3px;">PôpPôp</div>
                                            <div style="margin-top:6px;font-size:13px;color:#cbd5e1;">Mua sắm vui vẻ, kết nối yêu thương</div>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="padding:32px;">
                                            <h1 style="margin:0 0 20px;font-size:22px;line-height:1.4;color:#172554;">{safeSubject}</h1>
                                            <div style="font-size:15px;line-height:1.8;color:#475569;">{message}</div>
                                            <p style="margin:24px 0 0;font-size:13px;line-height:1.7;color:#64748b;">Nếu bạn không thực hiện yêu cầu này, vui lòng bỏ qua email. Để bảo vệ tài khoản, không chia sẻ liên kết này với người khác.</p>
                                            <p style="margin:24px 0 0;font-size:14px;line-height:1.7;color:#475569;">Trân trọng,<br><strong>Đội ngũ PôpPôp</strong></p>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="background-color:#f8fafc;padding:18px 32px;text-align:center;font-size:12px;line-height:1.6;color:#94a3b8;">
                                            Đây là email tự động, vui lòng không trả lời email này.<br>
                                            © {DateTime.UtcNow.Year} PôpPôp
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                    </table>
                </body>
                </html>
                """;
        }
    }
}

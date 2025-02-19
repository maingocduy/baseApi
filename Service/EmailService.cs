using AutoMapper;
using System;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using BaseApi.Extensions;

namespace BaseApi.Service
{
    public interface IEmailService
    {
        Task SendOtpEmailAsync(string to, string otp);
    }

    [ScopedService]
    public class EmailService : BaseService, IEmailService
    {
        private readonly string _smtpServer = "smtp.gmail.com"; // Thay đổi thành server SMTP của bạn
        private readonly int _smtpPort = 587; // Port SMTP
       
        private readonly string _fromEmail = "BaseApidemomobiplus@gmail.com"; // Email người gửi
        private readonly string _password = "gopg cauj lxki aeei"; // Mật khẩu của email người gửi

        public EmailService(Databases.TM.DBContext dbContext, IMapper mapper, IConfiguration configuration)
            : base(dbContext, mapper, configuration)
        {
            // Không cần khởi tạo SmtpClient ở đây nữa
        }

        public async Task SendEmailAsync(string to, string subject, string body)
        {
            try
            {
                var mailMessage = new MailMessage
                {
                    From = new MailAddress(_fromEmail, "Vietinbank"), // Tên hiển thị
                    Subject = subject,
                    Body = body,
                    IsBodyHtml = true 
                };

                mailMessage.To.Add(to);

                using (var smtpClient = new SmtpClient(_smtpServer, _smtpPort))
                {
                    smtpClient.Credentials = new NetworkCredential(_fromEmail, _password);
                    smtpClient.EnableSsl = true; 

                    // Gọi phương thức bất đồng bộ để gửi email
                    await smtpClient.SendMailAsync(mailMessage);
                }
            }
            catch (Exception ex)
            {
                // Xử lý lỗi (log lỗi hoặc thông báo cho người dùng)
                Console.WriteLine($"Failed to send email: {ex.Message}");
            }
        }

        private string GenerateOtpEmailTemplate(string otp)
        {
            return $@"
            <html>
            <head>
                <style>
                    body {{
                        font-family: Arial, sans-serif;
                        line-height: 1.6;
                        background-color: #f4f4f4;
                        padding: 20px;
                    }}
                    .container {{
                        background: #fff;
                        padding: 20px;
                        border-radius: 5px;
                        box-shadow: 0 2px 10px rgba(0, 0, 0, 0.1);
                    }}
                    h1 {{
                        color: #333;
                    }}
                    p {{
                        color: #555;
                    }}
                    .otp-code {{
                        font-size: 24px;
                        font-weight: bold;
                        color: #007BFF;
                    }}
                    footer {{
                        margin-top: 20px;
                        font-size: 12px;
                        color: #888;
                    }}
                </style>
            </head>
            <body>
                <div class='container'>
                    <h1>Chào Bạn,</h1>
                    <p>Bạn đã yêu cầu mã xác minh cho tài khoản của mình.</p>
                    <p>Mã OTP của bạn là:</p>
                    <p class='otp-code'>{otp}</p>
                    <p>Mã OTP này có hiệu lực trong 3 phút. Vui lòng không chia sẻ mã này với bất kỳ ai.</p>
                    <footer>
                        <p>Cảm ơn bạn,<br>Đội ngũ Hỗ trợ</p>
                    </footer>
                </div>
            </body>
            </html>";
        }

        public async Task SendOtpEmailAsync(string to, string otp)
        {
            string subject = "Mã OTP Xác Minh Tài Khoản của Bạn";
            string body = GenerateOtpEmailTemplate(otp);

            await SendEmailAsync(to, subject, body); // Gọi phương thức bất đồng bộ
        }
    }
}

using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using PlantShop.Business.Services.Abstractions.Main;
using System.Net;
using System.Net.Mail;
using System.Text;

namespace PlantShop.Business.Services.Implementations.Main
{
    public class MailService : IMailService
    {

        private readonly IConfiguration _configuration;
        private readonly IHostingEnvironment _webHostEnvironment;

        public MailService(IConfiguration configuration, IHostingEnvironment hostingEnvironment)
        {
            _configuration = configuration;
            _webHostEnvironment = hostingEnvironment;
        }

        public async Task SendEmailConfirmMail(string to, string userName, string confirmToken)
        {
            StringBuilder mail = new();
            mail.AppendLine("Salam<br>Sifarişiniz alındı. Sizinlə ən qısa zamanda əlaqə saxlanılacaq.");
            await SendMailAsync(to, "Confirm Mail", mail.ToString());
        }

        public async Task SendMailAsync(string to, string subject, string body, bool isBodyHtml = true)
        {
            await SendMailAsync(new[] { to }, subject, body, isBodyHtml);
        }

        public async Task SendMailWithAttachmentsAsync(string to, string subject, string body, string fileName, bool isBodyHtml = true)
        {
            await SendMailWithAttachmentsAsync(new[] { to }, subject, body, fileName, isBodyHtml);
        }

        public async Task SendMailAsync(string[] tos, string subject, string body, bool isBodyHtml = true)
        {
            MailMessage mail = new();
            mail.IsBodyHtml = isBodyHtml;
            foreach (var to in tos)
                mail.To.Add(to);
            mail.Subject = subject;
            mail.Body = body;
            //mail.Attachments.Add(new Attachment(fileName));
            mail.From = new(_configuration["EmailSender:Username"], "FloralFlights", System.Text.Encoding.UTF8);
            SmtpClient smtp = new("smtp.server.com");
            smtp.Credentials = new NetworkCredential(_configuration["EmailSender:Username"], _configuration["EmailSender:Password"]);
            smtp.Port = int.Parse(_configuration["EmailSender:Port"]);
            smtp.Host = _configuration["EmailSender:Host"];
            await smtp.SendMailAsync(mail);
        }

        public async Task SendMailWithAttachmentsAsync(string[] tos, string subject, string body, string fileName, bool isBodyHtml = true)
        {
            MailMessage mail = new();
            mail.IsBodyHtml = isBodyHtml;
            foreach (var to in tos)
                mail.To.Add(to);
            mail.Subject = subject;
            mail.Body = body;
            mail.Attachments.Add(new Attachment(@$"{_webHostEnvironment.WebRootPath}\{fileName}"));
            mail.From = new(_configuration["EmailSender:Username"], "Faina Zuckerman", System.Text.Encoding.UTF8);
            SmtpClient smtp = new();
            smtp.Credentials = new NetworkCredential(_configuration["EmailSender:Username"], _configuration["EmailSender:Password"]);
            smtp.Port = int.Parse(_configuration["EmailSender:Port"]);
            smtp.Host = _configuration["EmailSender:Host"];
            await smtp.SendMailAsync(mail);
        }

        public async Task SendPasswordResetMail(string to, string userName, string resetToken)
        {
            StringBuilder mail = new();
            mail.AppendLine("Salam<br>Şifrənizi yeniləmək üçün aşağıdaki linkə klikləyin.<br><strong><a target=\"blank\" href=\"");
            mail.Append(_configuration["AdminClientUrl"]);
            mail.Append("/ResetPassword?userName=");
            mail.Append(userName);
            mail.Append("&resetToken=");
            mail.Append(resetToken);
            mail.Append("\">Reset Password</a></strong>");
            mail.AppendLine("<br><strong>Istifadəçi adınız:</ strong >");
            mail.Append(userName);
            await SendMailAsync(to, "Reset Password", mail.ToString(), true);
        }
    }
}

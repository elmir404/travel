namespace PlantShop.Business.Services.Abstractions.Main
{
    public interface IMailService
    {
        Task SendMailAsync(string to, string subject, string body, bool isBodyHtml = true);
        //Task SendMailWithAttachmentsAsync(string to, string subject, string body,string fileName, bool isBodyHtml = true);
        Task SendMailAsync(string[] tos, string subject, string body, bool isBodyHtml = true);
        Task SendMailWithAttachmentsAsync(string[] tos, string subject, string body, string fileName, bool isBodyHtml = true);
        Task SendPasswordResetMail(string to, string userName, string resetToken);
        Task SendEmailConfirmMail(string to, string userName, string confirmToken);
    }
}

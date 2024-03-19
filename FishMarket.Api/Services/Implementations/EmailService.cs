using System.Net.Mail;
using System.Net;
using FishMarket.Api.Services.Contracts;
using FishMarket.Api.Helpers;
using Microsoft.Extensions.Options;
using FishMarket.Api.Utils;

namespace FishMarket.Api.Services.Implementations;

public class EmailService(IOptions<Smtp> smtp) : IEmailService
{
    public async Task SendAsync(string mailValidatorKey, string toEmail)
    {
        const string resourcePath = "Resources";
        const string resourceFileName = "MailValidator.html";
        const string subject = "FishMarket OTP";

        var smtpClient = new SmtpClient()
        {
            Port = smtp.Value.Port,
            Host = smtp.Value.Host,
            EnableSsl = smtp.Value.EnableSsl,
            Credentials = new NetworkCredential(smtp.Value.Sender.Email, smtp.Value.Sender.Password)
        };

        var bodyTemplate = ResourceHelper.ReadResource(resourcePath, resourceFileName);
        var body = bodyTemplate.Replace("@KEY", mailValidatorKey);

        var mail = new MailMessage()
        {
            From = new MailAddress(smtp.Value.Sender.Email, subject),
            Subject = subject,
            IsBodyHtml = true,
            Body = body
        };

        mail.To.Add(toEmail);

        await smtpClient.SendMailAsync(mail);
    }
}

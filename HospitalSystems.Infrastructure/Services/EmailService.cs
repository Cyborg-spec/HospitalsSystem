using System.Net;
using System.Net.Mail;
using HospitalSystems.Application.Common.Interfaces;
using HospitalSystems.Infrastructure.Services.Settings;
using Microsoft.Extensions.Options;

namespace HospitalSystems.Infrastructure.Services;

public class EmailService(IOptions<EmailSettings>settings):IEmailService
{
    private readonly EmailSettings _settings = settings.Value;

    public async Task SendAsync(string toEmail, string subject, string body)
    {
        using var client = new SmtpClient(_settings.SmtpHost, _settings.SmtpPort)
        {
            EnableSsl = true,
            Credentials = new NetworkCredential(_settings.Username, _settings.Password)
        };
        var message = new MailMessage
        {
            From = new MailAddress(_settings.FromEmail, _settings.FromName),
            Subject = subject,
            Body = body,
            IsBodyHtml = false
        };
        message.To.Add(toEmail);
        await client.SendMailAsync(message);
    }
}

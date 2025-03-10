using Microsoft.AspNetCore.Identity.UI.Services;
using System.Net.Mail;
using System.Net;
using Microsoft.Extensions.Options;
using RMIPSS_System.Configuration;

namespace RMIPSS_System.Services;

public class EmailSender(IOptions<EmailConfiguration> emailSettings) : IEmailSender
{
    private readonly string _smtpServer = emailSettings.Value.SmtpServer;
    private readonly int _smtpPort = emailSettings.Value.SmtpPort;
    private readonly string _smtpUsername = emailSettings.Value.SmtpUsername;
    private readonly string _smtpPassword = emailSettings.Value.SmtpPassword;
    private readonly string _fromEmail = emailSettings.Value.FromEmail;

    public async Task SendEmailAsync(string email, string subject, string htmlMessage)
    {
        var mailMessage = new MailMessage
        {
            From = new MailAddress(_fromEmail),
            Subject = subject,
            Body = htmlMessage,
            IsBodyHtml = true
        };
        mailMessage.To.Add(email);

        using var smtpClient = new SmtpClient(_smtpServer, _smtpPort);
        smtpClient.Credentials = new NetworkCredential(_smtpUsername, _smtpPassword);
        smtpClient.EnableSsl = true;
        await smtpClient.SendMailAsync(mailMessage);
    }
}

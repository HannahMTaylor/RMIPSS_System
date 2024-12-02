using Microsoft.AspNetCore.Identity.UI.Services;
using System.Net.Mail;
using System.Net;
using Microsoft.Extensions.Options;
using RMIPSS_System.Configuration;

namespace RMIPSS_System.Services;

public class EmailSender : IEmailSender
{
    private readonly string smtpServer;
    private readonly int smtpPort;
    private readonly string smtpUsername;
    private readonly string smtpPassword;
    private readonly string fromEmail;

    public EmailSender(IOptions<EmailConfiguration> emailSettings)
    {
        smtpServer = emailSettings.Value.SmtpServer;
        smtpPort = emailSettings.Value.SmtpPort;
        smtpUsername = emailSettings.Value.SmtpUsername;
        smtpPassword = emailSettings.Value.SmtpPassword;
        fromEmail = emailSettings.Value.FromEmail;
    }

    public async Task SendEmailAsync(string email, string subject, string htmlMessage)
    {
        var mailMessage = new MailMessage
        {
            From = new MailAddress(fromEmail),
            Subject = subject,
            Body = htmlMessage,
            IsBodyHtml = true
        };
        mailMessage.To.Add(email);

        using (var smtpClient = new SmtpClient(smtpServer, smtpPort))
        {
            smtpClient.Credentials = new NetworkCredential(smtpUsername, smtpPassword);
            smtpClient.EnableSsl = true;
            await smtpClient.SendMailAsync(mailMessage);
        }
    }
}

using Microsoft.AspNetCore.Identity.UI.Services;
using System.Net.Mail;
using System.Net;

namespace RMIPSS_System.Services;

public class EmailSender : IEmailSender
{
    private readonly string smtpServer = "mail.smtp2go.com";
    private readonly int smtpPort = 2525;
    private readonly string smtpUsername = "rmipss";
    private readonly string smtpPassword = "s1cRNuo8GRQDnmlD";

    public async Task SendEmailAsync(string email, string subject, string htmlMessage)
    {
        var fromEmail = "capstone-rmipss@yopmail.com";
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

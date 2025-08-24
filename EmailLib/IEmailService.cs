using MailKit.Security;
using MimeKit;

namespace CaferLib.EmailLib;

public interface IEmailService
{
    MimeMessage BuildEmail(string recipientEmail, string? recipientName, string subject, string body); 
    Task<Exception?> SendEmailAsync(MimeMessage emailMessage, SecureSocketOptions secureSocketOptions);
}
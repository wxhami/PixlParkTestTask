using System.Net;
using System.Net.Mail;
using Microsoft.Extensions.Options;
using ServerService.Options;

namespace ServerService;

public class SmtpClientFabric(IOptions<EmailOptions> options) : ISmtpClientFabric
{
    public SmtpClient Create()
    {
        return new SmtpClient(options.Value.SmtpHost, options.Value.SmtpPort)
        {
            EnableSsl = true,
            Credentials = new NetworkCredential(options.Value.MailSender, options.Value.Password)
        };
    }
}
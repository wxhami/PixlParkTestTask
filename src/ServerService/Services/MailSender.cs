using System.Net.Mail;
using Contracts;
using Microsoft.Extensions.Options;
using ServerService.Options;
using ServerService.Redis;

namespace ServerService;

public class MailSender(ISmtpClientFabric fabric, IOptions<EmailOptions> options) : IMailSender
{
    public async Task SendMessageAsync(string mailRecipient, string subject, string message)
    {
        using var client = fabric.Create();

        await client.SendMailAsync(new MailMessage(from: options.Value.MailSender, to: mailRecipient, subject,
            message));
    }
}
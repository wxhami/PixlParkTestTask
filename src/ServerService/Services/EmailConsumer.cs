using Contracts;
using MassTransit;
using ServerService.Redis;

namespace ServerService;

public class EmailConsumer(ICashService cashService, IMailSender mailSender) : IConsumer<EmailMessage>
{
    public async Task Consume(ConsumeContext<EmailMessage> context)
    {
        var mailRecipient = context.Message.Email;

        var code = Random.Shared.Next(1000, 10000).ToString();

        var message = $"Ваш код: {code}";

        await cashService.AddAsync(mailRecipient, code);

        await mailSender.SendMessageAsync(mailRecipient, "Auth code", message);
    }
}
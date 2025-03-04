namespace ServerService;

public interface IMailSender
{
    public Task SendMessageAsync(string mailRecipient, string subject, string message);
}
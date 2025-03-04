using System.Net.Mail;

namespace ServerService;

public interface ISmtpClientFabric
{
    SmtpClient Create();
}
using Contracts;

namespace WebClient;

public interface IServerServiceClient
{
    Task<CodeInformation?> GetCodeInfoAsync(string email);
}
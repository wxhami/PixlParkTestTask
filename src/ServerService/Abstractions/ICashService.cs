using Contracts;

namespace ServerService.Redis;

public interface ICashService
{
    Task AddAsync(string email, string code);

    Task<CodeInformation?> GetAsync(string email);
}
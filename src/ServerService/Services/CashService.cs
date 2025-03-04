using System.Text.Json;
using Contracts;
using StackExchange.Redis;

namespace ServerService.Redis;

public class CashService(IConnectionMultiplexer connectionMultiplexer, ILogger<CashService> logger) : ICashService
{
    public async Task AddAsync(string email, string code)
    {
        var db = connectionMultiplexer.GetDatabase();

        logger.LogDebug("Adding code information for email: {Email}", email);

        await db.StringSetAsync(email, code, TimeSpan.FromMilliseconds(30000));

        logger.LogDebug("Code information added successfully for email: {Email}", email);
    }
    
    public async Task<CodeInformation?> GetAsync(string email)
    {
        var db = connectionMultiplexer.GetDatabase();

        logger.LogDebug("Retrieving code information for email: {Email}", email);

        string code = (await db.StringGetAsync(email))!;

        if (string.IsNullOrEmpty(code))
        {
            logger.LogDebug("No code information found for email: {Email}", email);
            return null;
        }

        logger.LogInformation("Code information retrieved successfully for email: {Email}", email);

        return new CodeInformation()
        {
            Code = code,
            Email = email
        };
    }
}
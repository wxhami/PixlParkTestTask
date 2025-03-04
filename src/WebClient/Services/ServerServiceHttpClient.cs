using System.Net;
using System.Text.Json;
using Contracts;
using Microsoft.Extensions.Options;
using WebClient.Options;

namespace WebClient;

public class ServerServiceHttpClient(HttpClient httpClient, IOptions<ServerServiceHttpClientOptions> options) : IServerServiceClient
{
    public async Task<CodeInformation?> GetCodeInfoAsync(string email)
    {
        var response = await httpClient.GetAsync(string.Format(options.Value.CodeInfo,email));

        if (response.StatusCode is not HttpStatusCode.OK) return null;
        var result = await response.Content.ReadAsStringAsync();
        
        return JsonSerializer.Deserialize<CodeInformation>(result);

    }
}
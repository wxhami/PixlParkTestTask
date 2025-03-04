using System.ComponentModel.DataAnnotations;

namespace WebClient.Options;

public class ServerServiceHttpClientOptions
{
    [Required] 
    public string BaseUrl { get; set; } = null!;
    [Required] 
    public string CodeInfo { get; set; } = null!;

}
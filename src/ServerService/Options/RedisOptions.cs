using System.ComponentModel.DataAnnotations;

namespace ServerService.Options;

public class RedisOptions
{
    [Required] public string ConnectionString { get; set; } = null!;
}
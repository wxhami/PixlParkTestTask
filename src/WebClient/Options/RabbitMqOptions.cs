using System.ComponentModel.DataAnnotations;

namespace WebClient.Options;

public class RabbitMqOptions
{
    [Required]
    public string Username { get; set; } = null!;

    [Required]
    public string Password { get; set; } = null!;

    [Required]
    public string Host { get; set; } = null!;

    [Required]
    public string VirtualHost { get; set; } = null!;
    [Required]
    public ushort Port { get; set; }
}
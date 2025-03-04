using System.ComponentModel.DataAnnotations;

namespace Contracts;

public class EmailMessage
{
   public string Email { get; set; } = null!;
}
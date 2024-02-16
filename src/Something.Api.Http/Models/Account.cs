using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Something.Api;

public class Account
{
    [Required]
    [RegularExpression(
        pattern: "([a-zA-Z0-9]){5,15}", 
        ErrorMessage = "username must be 5-15 characters")]
    public string? Username { get; set; }

    [Required]
    public string? Password { get; set; }
}

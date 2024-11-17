using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace RMIPSS_System.Models.ProcessSteps;

public class User
{
    [MaxLength(50)]
    [DisplayName("First Name")]
    public string FirstName { get; set; } = String.Empty;
    [MaxLength(50)]
    [DisplayName("Last Name")]
    public string LastName { get; set; } = String.Empty;
    [MaxLength(256)]
    public string Email { get; set; } = String.Empty;
    [DisplayName("Phone Number")]
    public string? PhoneNumber { get; set; }
    [DataType(DataType.Password)]
    [RegularExpression(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[@$!%*?&])[A-Za-z\d@$!%*?&]{8,}$",
        ErrorMessage = "Password must be at least 8 characters long, include an uppercase letter, a lowercase letter, a number, and a special character.")]
    public string Password { get; set; } = String.Empty;
    [DisplayName("Confirm Password")]
    [DataType(DataType.Password)]
    [Compare("Password", ErrorMessage = "The Password and Confirm Password do not match.")]
    public string ConfirmPassword { get; set; } = String.Empty;
    public string Role { get; set; } = String.Empty;
}

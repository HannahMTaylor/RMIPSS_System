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
    public string Password { get; set; } = String.Empty;
    [DisplayName("Confirm Password")]
    public string ConfirmPassword { get; set; } = String.Empty;
    public string Role { get; set; } = String.Empty;
}

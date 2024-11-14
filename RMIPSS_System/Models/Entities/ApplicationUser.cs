using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace RMIPSS_System.Models.Entities;

public class ApplicationUser : IdentityUser
{
    [MaxLength(50)]
    public string FirstName { get; set; } = String.Empty;
    [MaxLength(50)]
    public string LastName { get; set; } = String.Empty;
}

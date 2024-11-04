using System.ComponentModel.DataAnnotations;

namespace RMIPSS_System.Models.Entities;

public class School
{
    [Key]
    public int Id { get; set; }
    [Required]
    public string Name { get; set; } = String.Empty;
    public string? Address { get; set; }
    public string? Phone { get; set; }
}

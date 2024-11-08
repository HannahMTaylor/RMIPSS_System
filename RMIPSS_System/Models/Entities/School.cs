using System.ComponentModel.DataAnnotations;

namespace RMIPSS_System.Models.Entities;

public class School
{
    public int Id { get; set; }
    [MaxLength(150)]
    public string Name { get; set; } = String.Empty;
    [MaxLength(255)]
    public string? Address { get; set; }
    [MaxLength(15)]
    public string? Phone { get; set; }
}

using System.ComponentModel.DataAnnotations;

namespace RMIPSS_System.Models.Entities;

public class Employee
{
    public int Id { get; set; }
    [MaxLength(50)]
    public string LastName { get; set; }
    [MaxLength(50)]
    public string FirstName { get; set; }

    // will also need to set up roles for either Teacher or State Level Employee
    // if Teacher: needs a School attribute -- could set "State" as a type of school affiliation for now
    [MaxLength(150)]
    public string Affiliation { get; set; } //use this (for now?) to determine which school a teacher is with or if State employee
    [MaxLength(150)]
    public string? TeachingLicenseType { get; set; }

}

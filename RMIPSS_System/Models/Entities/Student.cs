using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using RMIPSS_System.Models.Enums;

namespace RMIPSS_System.Models.Entities;

public class Student
{
    public int Id { get; set; }
    [MaxLength(50)]
    public string FirstName { get; set; } = String.Empty;
    public char? MiddleInitial { get; set; }
    [MaxLength(50)]
    public string LastName { get; set; } = String.Empty;
    [MaxLength(100)]
    public string Village { get; set; } = String.Empty;
    [MaxLength(100)]
    public string Atoll { get; set; } = String.Empty;
    public int PoBoxNo { get; set; }
    [MaxLength(100)]
    public string FatherName { get; set; } = String.Empty;
    [MaxLength(100)]
    public string MotherName { get; set; } = String.Empty;
    [MaxLength(100)]
    public string? GuardianName { get; set; }
    [MaxLength(15)]
    public string Phone { get; set; } = String.Empty;
    [MaxLength(255)]
    public string Email { get; set; } = String.Empty;
    public char Sex { get; set; }
    public int Age { get; set; }
    public DateOnly DOB { get; set; }
    [MaxLength(20)]
    public string HospitalNo { get; set; } = String.Empty;
    [MaxLength(11)]
    public string SSN { get; set; } = String.Empty;
    public int Grade { get; set; }
    public int? SchoolId { get; set; }
    [ValidateNever]
    public School? School { get; set; }
    [MaxLength(50)]
    public string PrimaryLanguage { get; set; } = String.Empty;
    [MaxLength(50)]
    public string ParentGuardianPrimaryLanguage { get; set; } = String.Empty;
    /// <summary>
    /// Recently completed special education form of the Student
    /// </summary>
    public SEProcessSteps SEProcessSteps { get; set; } 
    public DateOnly SEProcessCompletedDate { get; set; }
}

using System.ComponentModel.DataAnnotations;

namespace RMIPSS_System.Models.Entities;

public class Student
{
    [Key]
    public int Id { get; set; }
    
    [Required]
    [Display(Name = "First Name")]
    public string FirstName { get; set; } = String.Empty;
    [Display(Name = "Middle Initial")]
    public char? MiddleInitial { get; set; }
    [Required]
    [Display(Name = "Last Name")]
    public string LastName { get; set; } = String.Empty;

    [Required]
    public string Village { get; set; } = String.Empty;
    [Required]
    public string Atoll { get; set; } = String.Empty;
    [Required]
    [Display(Name = "P.O. Box No.")]
    public int PoBoxNo { get; set; }

    [Required]
    [Display(Name = "Father's Name")]
    public string FatherName { get; set; } = String.Empty;
    [Required]
    [Display(Name = "Mother's Name")]
    public string MotherName { get; set; } = String.Empty;
    public string? GuardianName { get; set; }

    [Required]
    public string Phone { get; set; } = String.Empty;
    [Required]
    public string Email { get; set; } = String.Empty;
    [Required]
    public char Sex { get; set; }
    [Required]
    public int Age { get; set; }
    [Required]
    [Display(Name = "Date of Birth")]
    public DateOnly DOB { get; set; }
    [Required]
    [Display(Name = "Hospital No.")]
    public string HospitalNo { get; set; } = String.Empty;
    [Required]
    [Display(Name = "Social Security No.")]
    public string SSN { get; set; } = String.Empty;
    [Required]
    public int Grade { get; set; }
    [Required]
    public string School { get; set; } = String.Empty;
    [Required]
    [Display(Name = "Primary Language of Child/Student")]
    public string PrimaryLanguage { get; set; } = String.Empty;
    [Required]
    [Display(Name = "Primary Language of Parent or Guardian")]
    public string ParentGuardianPrimaryLanguage { get; set; } = String.Empty;
}

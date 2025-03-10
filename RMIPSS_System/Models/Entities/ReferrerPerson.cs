using RMIPSS_System.Models.ProcessSteps;
using System.ComponentModel.DataAnnotations;

namespace RMIPSS_System.Models.Entities;

public class ReferrerPerson
{
    public int ID { get; set; }
    [MaxLength(100)]
    public string FullName { get; set; }
    [MaxLength(50)]
    public string? LastName { get; set; }
    [MaxLength(50)]
    public string? FirstName { get; set; }
    [MaxLength(50)]
    public string? RelationshipToStudent { get; set; }
    //public Student Student { get; set; }
    //public Referral Referral { get; set; }
    [MaxLength(50)]
    public string Phone {  get; set; }
    [MaxLength(100)]
    public string Email { get; set; }
    public DateOnly DateFilledReferral { get; set; }
}

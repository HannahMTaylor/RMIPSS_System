using RMIPSS_System.Models.Entities;
using System.ComponentModel.DataAnnotations;

namespace RMIPSS_System.Models.ProcessSteps;

public class SE2
{
    public int Id { get; set; }
    public Student Student { get; set; }
    [MaxLength(100)]
    public string CompletedByName { get; set; } = String.Empty;
    [MaxLength(50)]
    public string? CompletedByRelationship { get; set; }
    [MaxLength(15)]
    public string CompletedByPhone { get; set; } = String.Empty;
    [MaxLength(255)]
    public string CompletedByEmail { get; set; } = String.Empty;
    public DateOnly CompletedDate { get; set; }
    [MaxLength(50)]
    public string? PhysicalConcerns { get; set; }
    public string? OtherPhysicalConcerns { get; set; }
    [MaxLength(50)]
    public string? VisionConcerns { get; set; }
    public string? OtherVisionConcerns { get; set; }
    [MaxLength(50)]
    public string? HearingConcerns { get; set; }
    public string? OtherHearingConcerns { get; set; }
    [MaxLength(50)]
    public string? LanguageSpeechConcerns { get; set; }
    public string? OtherLanguageSpeechConcerns { get; set; }
    [MaxLength(50)]
    public string? BehaviorConcerns { get; set; }
    public string? OtherBehaviorConcerns { get; set; }
    [MaxLength(50)]
    public string? AcademicConcerns { get; set; }
    public string? OtherAcademicConcerns { get; set; }
    [MaxLength(50)]
    public string? OtherConcerns { get; set; }
    public string? OtherOtherConcerns { get; set; }
    public string? Comments { get; set; }
}

using System.ComponentModel.DataAnnotations;
using RMIPSS_System.Models.Entities;
using RMIPSS_System.Models.Enums;

namespace RMIPSS_System.Models.ProcessSteps;

public class ConsentForm
{
    
    public int Id { get; set; }

    public DateOnly EnteredDate { get; set; } = DateOnly.FromDateTime(DateTime.Now);

    [MaxLength(150)] public string? To { get; set; } = String.Empty;

    [MaxLength(150)] public string? From { get; set; } = String.Empty;

    /// <summary>
    /// true - for initial evaluation
    /// false - for reevaluation
    /// </summary>
    public Boolean? Evaluation { get; set; }
   
    public ConsentOption ConsentOption { get; set; } = ConsentOption.NotSpecified;

    public int StudentId { get; set; }
    public Student? Student { get; set; }

    public Boolean Status { get; set; }

    public DateOnly SubmittedDate { get; set; } = DateOnly.FromDateTime(DateTime.Now);
}
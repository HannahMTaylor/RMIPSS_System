using System.ComponentModel.DataAnnotations;
using RMIPSS_System.Models.Enums;

namespace RMIPSS_System.Models.ViewModel;

public class ConsentFormViewModel
{
    public int ConsentId { get; set; }
    public int StudentId { get; set; }
    
    public string? To { get; set; }

    public string? From { get; set; }
    public bool Status { get; set; }
    public DateOnly SubmittedDate { get; set; }
    public DateOnly EnteredDate { get; set; }
    public bool? Evaluation { get; set; }
    public int ConsentOption { get; set; } 
    public int Version { get; set; }
}

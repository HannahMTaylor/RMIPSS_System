using System.ComponentModel.DataAnnotations;
using RMIPSS_System.Models.Enums;

namespace RMIPSS_System.Models.Entities;

public class ConsentForm
{
    public int Id { get; set; }
    
    public DateOnly Date { get; set; }
    
    [MaxLength(150)]
    public string To { get; set; }
    
    [MaxLength(150)]
    public string From { get; set; }
    
    public Boolean Evaluation { get; set; }
    
    public ConsentOption ConsentOption { get; set; }
}
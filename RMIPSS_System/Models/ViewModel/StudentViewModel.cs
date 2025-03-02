using RMIPSS_System.Models.Entities;
using RMIPSS_System.Models.Enums;

namespace RMIPSS_System.Models.ViewModel;

public class StudentViewModel
{
    public int Id { get; set; }
    
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public DateOnly DOB { get; set; }
    public int Grade { get; set; }
    public char Sex { get; set; }
    public SEProcessSteps SEProcessSteps { get; set; }
    public List<DocumentViewModel> documentsList = new List<DocumentViewModel>();
    public List<DocumentViewModel> upcomingSEForms = new List<DocumentViewModel>();
}
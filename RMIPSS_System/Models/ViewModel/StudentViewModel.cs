using RMIPSS_System.Models.Entities;
using RMIPSS_System.Models.Enums;

namespace RMIPSS_System.Models.ViewModel;

public class StudentViewModel
{
    public int Id { get; set; }
    
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string DOB { get; set; }
    public SEProcessSteps SEProcessSteps { get; set; }
    public Dictionary<string, int> documentsList = new Dictionary<string, int>();
    public List<SEProcessSteps> upcomingSEForms = new List<SEProcessSteps>();
}
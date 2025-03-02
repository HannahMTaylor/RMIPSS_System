using RMIPSS_System.Models.Entities;

namespace RMIPSS_System.Models.ViewModel;

public class StudentListViewModel
{
    public List<Student> Students { get; set; }
    public string SearchTerm { get; set; }
    public int TotalStudents { get; set; }
    public int PageSize { get; set; }
    public int CurrentPage { get; set; }
    public bool isStateUser { get; set; }
}
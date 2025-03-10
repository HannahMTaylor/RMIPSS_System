namespace RMIPSS_System.Models.ViewModel;

public class DocumentViewModel
{
    public int id { get; set; }
    public string name { get; set; }
    public string controller { get; set; } = "Home";
    public string method { get; set; } = "ProcessStep";
}
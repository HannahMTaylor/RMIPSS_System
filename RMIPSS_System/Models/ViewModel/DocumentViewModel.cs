namespace RMIPSS_System.Models.ViewModel;

public class DocumentViewModel
{
    public int Id { get; set; }
    public string? Name { get; set; }
    public string? Controller { get; set; } = "Home";
    public string? Method { get; set; } = "ProcessStep";
}
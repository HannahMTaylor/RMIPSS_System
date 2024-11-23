using Microsoft.AspNetCore.Mvc.Rendering;

namespace RMIPSS_System.Models.ProcessSteps;

public class AddEditUserViewModel
{
    public User? User { get; set; }
    public List<SelectListItem>? Roles { get; set; }
}

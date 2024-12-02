using Microsoft.AspNetCore.Mvc.Rendering;

namespace RMIPSS_System.Models.ViewModel;

public class AddEditUserViewModel
{
    public User? User { get; set; }
    public List<SelectListItem>? Roles { get; set; }
}

using Microsoft.AspNetCore.Mvc;

namespace RMIPSS_System.Controllers;

public class SE2Controller : Controller
{
    public IActionResult ScreeningInformationForm()
    {
        return View();
    }
}

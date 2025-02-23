using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RMIPSS_System.Services;

namespace RMIPSS_System.Controllers;

[Authorize(Roles = Constants.ROLE_STATE_AND_SCHOOL_USER)]
public class StudentController : Controller
{
    private readonly ILogger<StudentController> _logger;

    public StudentController(ILogger<StudentController> logger)
    {
        _logger = logger;
    }
    
    public IActionResult ListStudent()
    {
        return View();
    }
}
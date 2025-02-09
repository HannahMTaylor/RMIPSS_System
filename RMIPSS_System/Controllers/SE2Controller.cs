using Microsoft.AspNetCore.Mvc;
using RMIPSS_System.Models.Entities;
using RMIPSS_System.Models.ProcessSteps;
using RMIPSS_System.Services;

namespace RMIPSS_System.Controllers;

public class SE2Controller : Controller
{
    private readonly SE2Service _se2Service;
    private readonly ILogger<SE2Controller> _logger;

    public SE2Controller(SE2Service se2Service, ILogger<SE2Controller> logger)
    {
        _se2Service = se2Service;
        _logger = logger;
    }

    public async Task<IActionResult> ScreeningInformationForm(int studentId)
    {
        Student student = await _se2Service.GetStudent(studentId);
        if (student == null)
        {
            return NotFound();
        }

        var model = new SE2
        {
            Student = student,
            CompletedDate = DateOnly.FromDateTime(DateTime.UtcNow)
        };

        return View("ScreeningInformationForm", model);
    }
}

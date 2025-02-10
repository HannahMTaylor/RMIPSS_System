using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RMIPSS_System.Models.Entities;
using RMIPSS_System.Models.ProcessSteps;
using RMIPSS_System.Models.ViewModel;
using RMIPSS_System.Services;

namespace RMIPSS_System.Controllers;

[Authorize(Roles = Constants.ROLE_STATE_AND_SCHOOL_USER)]
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

        SE2 se2 = await _se2Service.GetSE2Data(studentId);
        if (se2 != null) {
            SE2ViewModel viewModel = new SE2ViewModel
            {
                Student = student,
                SE2 = se2
            };
            return View(viewModel);
        }

        ApplicationUser user = await _se2Service.GetLoggedInUser(User.Identity.Name);
        SE2ViewModel newModel = new SE2ViewModel
        {
            Student = student,
            SE2 = new SE2
            {
                CompletedByName = user.FirstName + " " + user.LastName,
                CompletedByPhone = user.PhoneNumber,
                CompletedByEmail = user.Email,
                CompletedDate = DateOnly.FromDateTime(DateTime.UtcNow)
            }
        };
        return View(newModel);
    }

    [HttpPost]
    public async Task<IActionResult> SaveScreeningInformationForm(SE2ViewModel se2Model)
    {
        se2Model.SE2.StudentId = se2Model.Student.Id;
        if (se2Model.SE2.Id == 0)
        {
            await _se2Service.saveFormData(se2Model.SE2);
        } else
        {
            await _se2Service.updateFromData(se2Model.SE2);
        }
        return RedirectToAction("List", "User");
    }
}

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RMIPSS_System.Models.Entities;
using RMIPSS_System.Models.Enums;
using RMIPSS_System.Models.ProcessSteps;
using RMIPSS_System.Models.ViewModel;
using RMIPSS_System.Services;

namespace RMIPSS_System.Controllers;
/// <summary>
/// Using primary constructors for classes, which simplify constructor declaration by allowing
/// you to declare parameters directly in the class declaration.
/// </summary>
/// <param name="se2Service"></param>
/// <param name="studentService"></param>
/// <param name="logger"></param>
[Authorize(Roles = Constants.ROLE_STATE_AND_SCHOOL_USER)]
public class Se2Controller(Se2Service se2Service, StudentService studentService, ILogger<Se2Controller> logger)
    : Controller
{
    public async Task<IActionResult?> ScreeningInformationForm([Bind(Prefix = "id")] int studentId)
    {
        try
        {
            Student? student = await se2Service.GetStudent(studentId);
            if (student == null)
            {
                logger.LogError("Error:: StudentId = {StudentId} does not exits.", studentId);
                TempData["error"] = "An error occurred while loading SE2 Form view. Please try again later.";
                return RedirectToAction("Error", "Home");
            }

            SE2? se2 = await se2Service.GetSe2Data(studentId);
            if (se2 != null)
            {
                Se2ViewModel viewModel = new Se2ViewModel
                {
                    Student = student,
                    Se2 = se2
                };
                return View(viewModel);
            }

            if (User.Identity?.Name != null)
            {
                ApplicationUser? user = await se2Service.GetLoggedInUser(User.Identity.Name);
                Se2ViewModel newModel = new Se2ViewModel
                {
                    Student = student,
                    Se2 = new SE2
                    {
                        CompletedByName = user?.FirstName + " " + user?.LastName,
                        CompletedByPhone = user?.PhoneNumber,
                        CompletedByEmail = user?.Email,
                        CompletedDate = DateOnly.FromDateTime(DateTime.UtcNow)
                    }
                };
                return View(newModel);
            }
        }
        catch (Exception ex) {
            logger.LogError(ex, "Error occurred while trying to load SE2 Form view.");
            TempData["error"] = "An error occurred while loading SE2 Form view. Please try again later.";
            return RedirectToAction("Error", "Home");
        }

        return null;
    }

    [HttpPost]
    public async Task<IActionResult?> SaveScreeningInformationForm(Se2ViewModel se2Model)
    {
        try
        {
            if (se2Model is { Se2: not null, Student: not null })
            {
                se2Model.Se2.StudentId = se2Model.Student.Id;
                if (se2Model.Se2.Id == 0)
                {
                    await se2Service.SaveFormData(se2Model.Se2);
                    logger.LogInformation("SE2 Form created successfully for {FirstName} {lastName}.",
                        se2Model.Student.FirstName, se2Model.Student.LastName);
                    TempData["success"] = "SE2 Form created successfully!";
                }
                else
                {
#pragma warning disable CS8602 // Dereference of a possibly null reference.
                    await se2Service.UpdateFormData(se2Model.Se2);
#pragma warning restore CS8602 // Dereference of a possibly null reference.
                    logger.LogInformation("SE2 Form updated successfully for {FirstName} {lastName}.",
                        se2Model.Student.FirstName, se2Model.Student.LastName);
                    TempData["success"] = "SE2 Form updated successfully!";
                }
            }

            if (se2Model.Student != null)
                await studentService.UpdateSeProcessSteps(se2Model.Student.Id, SEProcessSteps.SE1, SEProcessSteps.SE2);
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "An exception occurred while saving SE2 Form.");
            Console.WriteLine($"Exception occurred: {ex.Message}");
            Console.WriteLine($"Stack Trace: {ex.StackTrace}");
            TempData["error"] = "An unexpected error occurred. Please try again.";
        }

        if (se2Model.Student != null)
            return RedirectToAction("StudentViewDetails", "Student", new { id = se2Model.Student.Id });
        return null;
    }
}

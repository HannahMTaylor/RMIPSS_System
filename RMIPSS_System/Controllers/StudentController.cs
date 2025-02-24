using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RMIPSS_System.Models.ViewModel;
using RMIPSS_System.Services;

namespace RMIPSS_System.Controllers;

[Authorize(Roles = Constants.ROLE_STATE_AND_SCHOOL_USER)]
public class StudentController : Controller
{
    private readonly ILogger<StudentController> _logger;
    private readonly StudentService _studentService;

    public StudentController(ILogger<StudentController> logger, StudentService studentService)
    {
        _logger = logger;
        _studentService = studentService;
    }
    
    public IActionResult ListStudent()
    {
        return View();
    }
    
    public async Task<IActionResult>  StudentViewDetails([Bind(Prefix = "id")] int studentId)
    {
        if (studentId == null || studentId == 0)
        {
            TempData["error"] = "Please select a student";
            return RedirectToAction("Index","Home");
        }

        try
        {
            StudentViewModel studentViewModel = await _studentService.GetStudentByIdAsync(studentId);
            if (studentViewModel != null)
            {
                return View(studentViewModel);
            }
            else
            {
                _logger.LogError("Error while viewing details of " + studentViewModel.FirstName + " " + studentViewModel.LastName);
                TempData["error"] = "Error: Student Details is not loading, Please try again.";
            }
            
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "An exception occurred while viewing the student details");
            Console.WriteLine($"Exception occurred: {ex.Message}");
            Console.WriteLine($"Stack Trace: {ex.StackTrace}");
            TempData["error"] = "An unexpected error occurred. Please try again.";
        } 
        
        return RedirectToAction("Index","Home");


    }
            
    
}
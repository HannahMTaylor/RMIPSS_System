using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RMIPSS_System.Models.Entities;
using RMIPSS_System.Models.ViewModel;
using RMIPSS_System.Services;

namespace RMIPSS_System.Controllers;

[Authorize(Roles = Constants.ROLE_STATE_AND_SCHOOL_USER)]
public class StudentController : Controller
{
    private readonly ILogger<StudentController> _logger;
    private readonly StudentService _studentService;
    private readonly UserService _userService;

    public StudentController(ILogger<StudentController> logger, StudentService studentService, UserService userService)
    {
        _logger = logger;
        _studentService = studentService;
        _userService = userService;
    }
    
    public async Task<IActionResult> ListStudent(string search = "", int pageNo = 1, int pageSize = 10)
    {
        try
        {
            bool isStateUser = User.IsInRole(Constants.ROLE_STATE_USER);
            int? schoolId = null;

            // If user is not State user, get their school from the User table
            if (!isStateUser)
            {
                ApplicationUser user = await _userService.getUserByUsername(User.Identity.Name);
                schoolId = user.SchoolId;
            }

            var (students, totalStudents) =
                await _studentService.GetPaginatedStudentsAsync(search, schoolId, pageNo, pageSize);

            var viewModel = new StudentListViewModel
            {
                Students = students,
                SearchTerm = search,
                TotalStudents = totalStudents,
                PageSize = pageSize,
                CurrentPage = pageNo,
                isStateUser = isStateUser
            };

            return View(viewModel);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error occurred while trying to retrieve student list.");
            TempData["error"] = "An error occurred while loading the dashboard page. Please try again later.";
            return RedirectToAction("Error", "Home");
        }
    }

    public async Task<IActionResult> StudentViewDetails([Bind(Prefix = "id")] int studentId)
    {
        if (studentId == null || studentId == 0)
        {
            TempData["error"] = "Please select a student";
            return RedirectToAction("ListStudent", "Student");
        }
        try
        {
            bool isSchoolUser = User.IsInRole(Constants.ROLE_SCHOOL_USER);
            int? schoolId = null;
            if (isSchoolUser)
            {
                ApplicationUser user = await _userService.getUserByUsername(User.Identity.Name);
                schoolId = user.SchoolId;
            }

            StudentViewModel? studentViewModel = await _studentService.GetStudentByIdAsync(studentId, schoolId);
            if (studentViewModel != null)
            {
                if (studentViewModel.hasAccess)
                {
                    return View(studentViewModel);
                }

                if (!studentViewModel.hasAccess)
                {
                    TempData["error"] = "You are not authorized to view this page.";
                }
            }

            else
            {
                _logger.LogError("Error while viewing details of student");
                TempData["error"] = "Error: Error while viewing details of student, Please try again.";
            }
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "An exception occurred while viewing the student details");
            Console.WriteLine($"Exception occurred: {ex.Message}");
            Console.WriteLine($"Stack Trace: {ex.StackTrace}");
            TempData["error"] = "An unexpected error occurred. Please try again.";
        }

        return RedirectToAction("ListStudent", "Student");
    }
}
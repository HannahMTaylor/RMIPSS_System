using System.Diagnostics;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RMIPSS_System.Models.Entities;
using RMIPSS_System.Models.ViewModel;
using RMIPSS_System.Services;

namespace RMIPSS_System.Controllers;

/// <summary>
/// Using primary constructors for classes, which simplify constructor declaration by allowing
/// you to declare parameters directly in the class declaration.
/// </summary>
/// <param name="logger"></param>
/// <param name="studentService"></param>
/// <param name="userService"></param>
[Authorize(Roles = Constants.ROLE_STATE_AND_SCHOOL_USER)]
public class StudentController(
    ILogger<StudentController> logger,
    StudentService studentService,
    UserService userService)
    : Controller
{
    /// <summary>
    /// Retrieves a paginated list of students based on search criteria and user role.
    /// If the user is a State user, all students are returned; otherwise, only students from the user's school are retrieved.
    /// </summary>
    /// <param name="search">Optional search term to filter students by name.</param>
    /// <param name="pageNo">Current page number for pagination (default is 1).</param>
    /// <param name="pageSize">Number of students to display per page (default is 10).</param>
    /// <returns>A view containing a list of students along with pagination details.</returns>
    public async Task<IActionResult> ListStudent(string search = "", int pageNo = 1, int pageSize = 10)
    {
        try
        {
            bool isStateUser = User.IsInRole(Constants.ROLE_STATE_USER);
            int? schoolId = null;

            // If user is not State user, get their school from the User table
            if (!isStateUser)
            {
                if (User.Identity != null)
                {
                    ApplicationUser? user = await userService.GetUserByUsername(User.Identity.Name);
                    schoolId = user.SchoolId;
                }
            }

            var (students, totalStudents) =
                await studentService.GetPaginatedStudentsAsync(search, schoolId, pageNo, pageSize);

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
            logger.LogError(ex, "Error occurred while trying to retrieve student list.");
            TempData["error"] = "An error occurred while loading the dashboard page. Please try again later.";
            return RedirectToAction("Error", "Home");
        }
    }

    public async Task<IActionResult> StudentViewDetails([Bind(Prefix = "id")] int studentId)
    {
        if (studentId == 0)
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
                if (User.Identity != null)
                {
                    Debug.Assert(User.Identity.Name != null, "User.Identity.Name != null");
                    ApplicationUser? user = await userService.GetUserByUsername(User.Identity.Name);
                    schoolId = user.SchoolId;
                }
            }

            StudentViewModel? studentViewModel = await studentService.GetStudentByIdAsync(studentId, schoolId);
            if (studentViewModel != null)
            {
                if (studentViewModel.hasAccess)
                {
                    return View(studentViewModel);
                }

                if (!studentViewModel.hasAccess)
                {
                    return RedirectToPage("/Account/AccessDenied", new { area = "Identity" });

                }
            }

            else
            {
                logger.LogError("Error while viewing details of student");
                TempData["error"] = "Error: Error while viewing details of student, Please try again.";
            }
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "An exception occurred while viewing the student details");
            Console.WriteLine($"Exception occurred: {ex.Message}");
            Console.WriteLine($"Stack Trace: {ex.StackTrace}");
            TempData["error"] = "An unexpected error occurred. Please try again.";
        }

        return RedirectToAction("ListStudent", "Student");
    }
}
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
    
    public async Task<IActionResult> ListStudent(string search = "", int pageNo = 1, int pageSize = 10)
    {
        var (students, totalStudents) = await _studentService.GetPaginatedStudentsAsync(search, pageNo, pageSize);

        var viewModel = new StudentListViewModel
        {
            Students = students,
            SearchTerm = search,
            TotalStudents = totalStudents,
            PageSize = pageSize,
            CurrentPage = pageNo
        };

        return View(viewModel);
    }
}
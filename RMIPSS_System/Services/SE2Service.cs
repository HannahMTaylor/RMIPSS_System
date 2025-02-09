using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using RMIPSS_System.Models.Entities;
using RMIPSS_System.Repository.IRepository;

namespace RMIPSS_System.Services;

public class SE2Service
{
    private readonly ILogger<SE2Service> _logger;
    private readonly ISE2Repository _se2Repo;
    private readonly IStudentRepository _studentRepo;

    public SE2Service(ILogger<SE2Service> logger, ISE2Repository db, IStudentRepository studentDb)
    {
        _logger = logger;
        _se2Repo = db;
        _studentRepo = studentDb;
    }

    public async Task<Student> GetStudent(int studentId)
    {
        return await _studentRepo.GetAsync(student => student.Id == studentId);
    }

}

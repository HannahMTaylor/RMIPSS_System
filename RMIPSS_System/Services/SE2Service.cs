using RMIPSS_System.Models.Entities;
using RMIPSS_System.Models.ProcessSteps;
using RMIPSS_System.Repository.IRepository;

namespace RMIPSS_System.Services;

public class SE2Service
{
    private readonly ILogger<SE2Service> _logger;
    private readonly ISE2Repository _se2Repo;
    private readonly IStudentRepository _studentRepo;
    private readonly IApplicationUserRepository _appUserRepo;

    public SE2Service(ILogger<SE2Service> logger, ISE2Repository db, IStudentRepository studentDb, IApplicationUserRepository appUserDb)
    {
        _logger = logger;
        _se2Repo = db;
        _studentRepo = studentDb;
        _appUserRepo = appUserDb;
    }

    public async Task<Student> GetStudent(int studentId)
    {
        return await _studentRepo.GetAsync(student => student.Id == studentId);
    }

    public async Task<ApplicationUser> GetLoggedInUser(String username)
    {
        return await _appUserRepo.GetAsync(user => user.UserName.ToLower() == username.ToLower());
    }

    public async Task saveFormData(SE2 se2)
    {
        await _se2Repo.AddAsync(se2);
        await _se2Repo.SaveAsync();
    }

    public async Task<SE2> GetSE2Data(int studentId)
    {
        return await _se2Repo.GetAsync(se2 => se2.StudentId == studentId);
    }

    public async Task updateFromData(SE2 sE2)
    {
        _se2Repo.Update(sE2);
        await _se2Repo.SaveAsync();
    }
}

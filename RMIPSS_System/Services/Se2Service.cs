using RMIPSS_System.Models.Entities;
using RMIPSS_System.Models.ProcessSteps;
using RMIPSS_System.Repository.IRepository;

namespace RMIPSS_System.Services;

public class Se2Service(
    ILogger<Se2Service> logger,
    ISe2Repository db,
    IStudentRepository studentDb,
    IApplicationUserRepository appUserDb)
{
    private readonly ISe2Repository _se2Repo = db;
    private readonly IStudentRepository _studentRepo = studentDb;
    private readonly IApplicationUserRepository _appUserRepo = appUserDb;

    public async Task<Student?> GetStudent(int studentId)
    {
        try
        {
                return await _studentRepo.GetAsync(student => student != null && student.Id == studentId);
        }
        catch (Exception? ex)
        {
            logger.LogError(ex, "Error fetching student for SE2 Form.");
            throw;
        }
    }

    public async Task<ApplicationUser?> GetLoggedInUser(String username)
    {
        try
        {
                return await _appUserRepo.GetAsync(user =>
                    user != null && user.UserName != null && user.UserName.ToLower() == username.ToLower());
        }
        catch (Exception? ex)
        {
            logger.LogError(ex, "Error getting logged-in user for SE2 Form.");
            throw;
        }
    }

    public async Task SaveFormData(SE2? se2)
    {
        try
        {
                await _se2Repo.AddAsync(se2);
#pragma warning disable CS8602 // Dereference of a possibly null reference.
                await _se2Repo.SaveAsync();
#pragma warning restore CS8602 // Dereference of a possibly null reference.
        }
        catch (Exception? ex)
        {
           logger.LogError(ex, "Error saving form data for SE2 Form.");
            throw;
        }
    }

    public async Task<SE2?> GetSe2Data(int studentId)
    {
        try
        {
            return await _se2Repo.GetAsync(se2 => se2 != null && se2.StudentId == studentId);
        }
        catch (Exception? ex)
        {
            logger.LogError(ex, "Error getting form data for SE2 Form.");
            throw;
        }
    }

    public async Task? UpdateFormData(SE2? sE2)
    {
        try
        {
                _se2Repo.Update(sE2);
#pragma warning disable CS8602 // Dereference of a possibly null reference.
                await _se2Repo.SaveAsync();
#pragma warning restore CS8602 // Dereference of a possibly null reference.
        
        }
        catch (Exception? ex)
        { 
            logger.LogError(ex, "Error updating form data for SE2 Form.");
            throw;
        }
    }
}

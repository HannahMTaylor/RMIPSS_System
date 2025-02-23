using RMIPSS_System.Data;
using RMIPSS_System.Models.Entities;
using RMIPSS_System.Repository.IRepository;

namespace RMIPSS_System.Services;

public class StudentService
{
    private readonly ILogger<UserService> _logger;
    private readonly IStudentRepository _studentRepository;

    public StudentService(ILogger<UserService> logger, IStudentRepository studentRepository)
    {
        _logger = logger; 
        _studentRepository = studentRepository;
    }

    public async Task<Student> GetStudentByIdAsync(int id)
    {
        Student student = await _studentRepository.GetByStudentIdAsync(id);
        if (student == null)
        {
            _logger.LogError("Student with id: {id} not found", id);
        }
        
        return student;
    }
    
    
}
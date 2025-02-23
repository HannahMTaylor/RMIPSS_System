using RMIPSS_System.Models.Entities;
using RMIPSS_System.Repository.IRepository;

namespace RMIPSS_System.Services;

public class StudentService
{
    private readonly ILogger<StudentService> _logger;
    private readonly IStudentRepository _studentRepository;

    public StudentService(ILogger<StudentService> logger, IStudentRepository studentRepository)
    {
        _logger = logger; 
        _studentRepository = studentRepository;
    }

    // public async Task<Student> GetStudentByIdAsync(int id)
    // {
    //     Student student = await _studentRepository.GetByStudentIdAsync(id);
    //     if (student == null)
    //     {
    //         _logger.LogError("Student with id: {id} not found", id);
    //     }
    //     
    //     return student;
    // }


    public async Task<(List<Student>, int)> GetPaginatedStudentsAsync(string search, int pageNo, int pageSize)
    {
        return await _studentRepository.GetPaginatedStudentsAsync(search, pageNo, pageSize);
    }
}
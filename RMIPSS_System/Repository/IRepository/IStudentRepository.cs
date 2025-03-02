using RMIPSS_System.Models.Entities;

namespace RMIPSS_System.Repository.IRepository;

public interface IStudentRepository : IRepository<Student>
{
    Task<(List<Student>, int)> GetPaginatedStudentsAsync(string search, int? schoolId, int pageNo, int pageSize);
    Task<Student> GetByStudentIdAsync(int id);
    void Update(Student student);
    Task SaveAsync();
}

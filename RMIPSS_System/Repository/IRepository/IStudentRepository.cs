using RMIPSS_System.Models.Entities;

namespace RMIPSS_System.Repository.IRepository;

public interface IStudentRepository : IRepository<Student>
{

    Task<Student> GetByStudentIdAsync(int id);
}

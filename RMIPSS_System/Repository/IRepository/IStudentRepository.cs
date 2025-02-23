using RMIPSS_System.Models.Entities;

namespace RMIPSS_System.Repository.IRepository;

public interface IStudentRepository : IRepository<Student>
{
    Task<(List<Student>, int)> GetPaginatedStudentsAsync(string search, int pageNo, int pageSize);
}

using RMIPSS_System.Models.Entities;

namespace RMIPSS_System.Repository.IRepository;

public interface ISchoolRepository : IRepository<School>
{
    void Update(School school);
    void Save();

    // Asynchronous Functions
    Task SaveAsync();
}

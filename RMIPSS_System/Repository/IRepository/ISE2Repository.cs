using RMIPSS_System.Models.Entities;
using RMIPSS_System.Models.ProcessSteps;

namespace RMIPSS_System.Repository.IRepository;

public interface ISE2Repository : IRepository<SE2>
{
    void Update(SE2 se2);
    Task SaveAsync();
}

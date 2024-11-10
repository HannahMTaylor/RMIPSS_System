using RMIPSS_System.Models.Entities;

namespace RMIPSS_System.Repository.IRepository;

public interface IApplicationUserRepository : IRepository<ApplicationUser>
{
    void Update(ApplicationUser applicationUser);
    void Save();
}

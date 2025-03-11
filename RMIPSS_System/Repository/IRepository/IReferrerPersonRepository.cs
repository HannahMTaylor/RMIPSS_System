using RMIPSS_System.Models.Entities;

namespace RMIPSS_System.Repository.IRepository;

public interface IReferrerPersonRepository : IRepository<ReferrerPerson>
{
    void Update(ReferrerPerson referrerPerson);
    void Save();
    //Task<ReferrerPerson> GetReferrerPersonById(int id);

    Task<ReferrerPerson> SaveReferrerPersonAsync(ReferrerPerson referrerPerson);
    Task SaveAsync();
    //Task<ReferrerPerson> UpdateReferrerPersonAsync(ReferrerPerson referrerPerson);
}

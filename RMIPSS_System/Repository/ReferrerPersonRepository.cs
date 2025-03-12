using RMIPSS_System.Data;
using RMIPSS_System.Models.Entities;
using RMIPSS_System.Repository.IRepository;

namespace RMIPSS_System.Repository;

public class ReferrerPersonRepository : Repository<ReferrerPerson>, IReferrerPersonRepository
{
    private ApplicationDbContext _db;

    public ReferrerPersonRepository(ApplicationDbContext db) : base(db)
    {
        _db = db;
    }

    public void Update(ReferrerPerson person)
    {
        _db.ReferrerPeople.Update(person);
    }

    public void Save()
    {
        _db.SaveChanges();
    }

    //public async Task<ReferrerPerson> GetReferrerPersonById(int id)
    //{
    //    ReferrerPerson Person = await GetAsync(p => p.)
    //}


    public async Task<ReferrerPerson> SaveReferrerPersonAsync(ReferrerPerson person)
    {
        await _db.ReferrerPeople.AddAsync(person);
        await _db.SaveChangesAsync();
        return person;
    }

    public async Task SaveAsync()
    {
        await _db.SaveChangesAsync();
    }
}

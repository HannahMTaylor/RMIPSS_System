using RMIPSS_System.Data;
using RMIPSS_System.Models.Entities;
using RMIPSS_System.Repository.IRepository;

namespace RMIPSS_System.Repository;

public class SchoolRepository : Repository<School>, ISchoolRepository
{
    private readonly ApplicationDbContext _db;

    public SchoolRepository(ApplicationDbContext db) : base(db)
    {
        _db = db;
    }

    public void Save()
    {
        _db.SaveChanges();
    }

    public void Update(School school)
    {
        _db.Schools.Update(school);
    }

    // Asynchronous Functions
    public async Task SaveAsync()
    {
        await _db.SaveChangesAsync();
    }
}

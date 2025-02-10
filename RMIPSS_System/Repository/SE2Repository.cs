using RMIPSS_System.Data;
using RMIPSS_System.Models.Entities;
using RMIPSS_System.Models.ProcessSteps;
using RMIPSS_System.Repository.IRepository;

namespace RMIPSS_System.Repository;

public class SE2Repository : Repository<SE2>, ISE2Repository
{
    private readonly ApplicationDbContext _db;

    public SE2Repository(ApplicationDbContext db) : base(db)
    {
        _db = db;
    }

    public async Task SaveAsync()
    {
        await _db.SaveChangesAsync();
    }

    public void Update(SE2 se2)
    {
        _db.SE2.Update(se2);
    }
}

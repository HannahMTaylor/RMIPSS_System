using RMIPSS_System.Data;
using RMIPSS_System.Models.ProcessSteps;
using RMIPSS_System.Repository.IRepository;

namespace RMIPSS_System.Repository;

public class Se2Repository(ApplicationDbContext db) : Repository<SE2>(db), ISe2Repository
{
    private readonly ApplicationDbContext _db = db;


    public async Task? SaveAsync()
    {
        await _db.SaveChangesAsync();
    }

    public void Update(SE2? se2)
    {
        _db.SE2.Update(se2);
    }
}

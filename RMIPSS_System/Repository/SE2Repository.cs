using RMIPSS_System.Data;
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

    public Task SaveAsync()
    {
        throw new NotImplementedException();
    }

    public void Update(SE2 se2)
    {
        throw new NotImplementedException();
    }
}

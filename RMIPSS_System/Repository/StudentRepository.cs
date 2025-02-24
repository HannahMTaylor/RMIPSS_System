using RMIPSS_System.Data;
using RMIPSS_System.Models.Entities;
using RMIPSS_System.Repository.IRepository;

namespace RMIPSS_System.Repository;

public class StudentRepository : Repository<Student>, IStudentRepository
{
    private readonly ApplicationDbContext _db;

    public StudentRepository(ApplicationDbContext db) : base(db)
    {
        _db = db;
    }

    public async Task<Student> GetByStudentIdAsync(int id)
    {
        Student student = await GetAsync(s =>
            s.Id == id
        );
        return student;
    }
}

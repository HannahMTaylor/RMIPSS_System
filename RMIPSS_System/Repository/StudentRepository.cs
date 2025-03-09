using Microsoft.EntityFrameworkCore;
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
    
    public async Task<(List<Student>, int)> GetPaginatedStudentsAsync(string search, int? schoolId, int pageNo, int pageSize)
    {
        var query = _db.Students.AsQueryable();

        // Search across Student's FirstName, MiddleInitial, and LastName
        if (search != "")
        {
            query = query.Where(s =>
                (s.FirstName + " " + (s.MiddleInitial.HasValue ? s.MiddleInitial.ToString() + " " : "") + s.LastName).ToLower()
                .Contains(search.ToLower())
            );
        }
        
        // Filter by School
        if (schoolId.HasValue)
        {
            query = query.Where(s => s.SchoolId == schoolId);
        }

        // Get total number of students
        int totalStudents = await query.CountAsync();

        // Apply pagination
        var students = await query
            .OrderBy(s => s.FirstName)
            .Skip((pageNo - 1) * pageSize)
            .Take(pageSize)
            .Select(s => new Student
            {
                Id = s.Id,
                FirstName = s.FirstName,
                MiddleInitial = s.MiddleInitial,
                LastName = s.LastName,
                Email = s.Email,
                Grade = s.Grade,
                Village = s.Village,
                Atoll = s.Atoll,
                PoBoxNo = s.PoBoxNo,
                Phone = s.Phone,
                DOB = s.DOB,
                School = s.School,
                SEProcessSteps = s.SEProcessSteps,
                SEProcessCompletedDate = s.SEProcessCompletedDate
            })
            .ToListAsync();

        return (students, totalStudents);
    }
    
    public async Task<Student> GetByStudentIdAsync(int id)
    {
        Student student = await GetAsync(s =>
            s.Id == id
        );
        return student;
    }
    
    public void Update(Student student)
    {
        _db.Students.Update(student);
    }
    
    public async Task SaveAsync()
    {
        await _db.SaveChangesAsync();
    }
}

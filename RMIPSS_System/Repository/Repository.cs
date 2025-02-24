using Microsoft.EntityFrameworkCore;
using RMIPSS_System.Data;
using RMIPSS_System.Repository.IRepository;
using System.Linq.Expressions;
using RMIPSS_System.Models;

namespace RMIPSS_System.Repository;

public class Repository<T> : IRepository<T> where T : class
{
    private readonly ApplicationDbContext _db;
    internal DbSet<T> dbSet;

    public Repository(ApplicationDbContext db)
    {
        _db = db;
        dbSet = _db.Set<T>();
    }

    public void Add(T entity)
    {
        dbSet.Add(entity);
    }

    public T Get(Expression<Func<T, bool>> filter)
    {
        IQueryable<T> query = dbSet;
        query = query.Where(filter);
        return query.FirstOrDefault();
    }

    public IEnumerable<T> GetAll()
    {
        IQueryable<T> query = dbSet;
        return query.ToList();
    }

    public void Remove(T entity)
    {
        dbSet.Remove(entity);
    }
    
    public T GetById(int id)
    {
        return dbSet.Find(id);
    }

    public void RemoveById(int id)
    {
        dbSet.Remove(dbSet.Find(id));
    }

    public T Save(T entity)
    {
        _db.Add(entity);
        _db.SaveChanges();
        return entity;
    }

    public void Save()
    {
        _db.SaveChanges();
    }

    public  async Task<int> GetEntityIdByStudentId<T>(int id) where T : class, IStudentEntity
    {
            // Query the appropriate DbSet for the type U.
            var entityId = await _db.Set<T>()
                .Where(e => e.Student.Id == id)
                .Select(e => e.Id)
                .FirstOrDefaultAsync();
            return entityId;
    }


    // Asynchronous Functions
    public async Task AddAsync(T entity)
    {
        await dbSet.AddAsync(entity);
    }

    public async Task<T> GetAsync(Expression<Func<T, bool>> filter)
    {
        IQueryable<T> query = dbSet;
        query = query.Where(filter);
        return await query.FirstOrDefaultAsync();
    }

    public async Task<IEnumerable<T>> GetAllAsync()
    {
        IQueryable<T> query = dbSet;
        return await query.ToListAsync();
    }
}

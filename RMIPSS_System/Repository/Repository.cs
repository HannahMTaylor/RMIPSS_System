using Microsoft.EntityFrameworkCore;
using RMIPSS_System.Data;
using RMIPSS_System.Repository.IRepository;
using System.Linq.Expressions;
using RMIPSS_System.Models;

namespace RMIPSS_System.Repository;

public class Repository<T> : IRepository<T> where T : class
{
    private readonly ApplicationDbContext _db;
    private readonly DbSet<T> _dbSet;

    public Repository(ApplicationDbContext db)
    {
        _db = db;
         _dbSet = _db.Set<T>();
    }
    

    public void Add(T? entity)
    {
        if (entity != null) _dbSet.Add(entity);
    }

    public T? Get(Expression<Func<T?, bool>> filter)
    {
        IQueryable<T?> query = _dbSet;
        query = query.Where(filter);
        return query.FirstOrDefault();
    }

    public IEnumerable<T?> GetAll()
    {
        IQueryable<T?> query = _dbSet;
        return query.ToList();
    }

    public void Remove(T? entity)
    {
        if (entity != null) _dbSet.Remove(entity);
    }
    
    public async Task<T?> GetByIdAsync(int id)
    {
        return await _dbSet.FindAsync(id);
    }

    public void RemoveById(int id)
    {
        _dbSet.Remove(_dbSet.Find(id) ?? throw new InvalidOperationException());
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

    public  async Task<int> GetProcessStepIdByStudentId<TU>(int id) where TU : class, IStudentEntity
    {
            // Query the appropriate DbSet for the type U.
            var entityId = await _db.Set<TU>()
                .Where(e => e.Student != null && e.Student.Id == id)
                .Select(e => e.Id)
                .FirstOrDefaultAsync();
            return entityId;
    }


    // Asynchronous Functions
    public async Task AddAsync(T? entity)
    {
        if (entity != null) await _dbSet.AddAsync(entity);
    }

    public async Task<T?> GetAsync(Expression<Func<T?, bool>> filter)
    {
        IQueryable<T?> query = _dbSet;
        query = query.Where(filter);
        return await query.FirstOrDefaultAsync();
    }

    public async Task<IEnumerable<T?>> GetAllAsync()
    {
        IQueryable<T?> query = _dbSet;
        return await query.ToListAsync();
    }
}

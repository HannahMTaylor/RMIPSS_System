using Microsoft.EntityFrameworkCore;
using RMIPSS_System.Data;
using RMIPSS_System.Repository.IRepository;
using System.Linq.Expressions;

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

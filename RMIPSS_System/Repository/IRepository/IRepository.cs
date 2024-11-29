using System.Linq.Expressions;

namespace RMIPSS_System.Repository.IRepository;

public interface IRepository<T> where T : class
{
    IEnumerable<T> GetAll();
    T Get(Expression<Func<T, bool>> filter);
    void Add(T entity);
    void Remove(T entity);

    // Asynchronous Functions
    Task<IEnumerable<T>> GetAllAsync();
    Task<T> GetAsync(Expression<Func<T, bool>> filter);
    Task AddAsync(T entity);
    
    T GetById(int id);
    
    void RemoveById(int id);
    
    T Save(T entity);


    void Save();
    
}

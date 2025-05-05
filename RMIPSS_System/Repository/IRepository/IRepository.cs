using System.Linq.Expressions;
using RMIPSS_System.Models;

namespace RMIPSS_System.Repository.IRepository;

public interface IRepository<T> where T : class
{
    IEnumerable<T?> GetAll();
    T? Get(Expression<Func<T?, bool>> filter);
    void Add(T? entity);
    Task Remove(T? entity);

    // Asynchronous Functions
    Task<IEnumerable<T?>> GetAllAsync();
    Task<T?> GetAsync(Expression<Func<T?, bool>> filter);
    Task AddAsync(T? entity);
    
    Task<T?> GetByIdAsync(int id);
    
    void RemoveById(int id);
    
    Task<T?> Save(T entity);
    
    Task RemoveByIdAsync(int id);
    
    Task Save();
    
   // Task<int> GetProcessStepIdByStudentId<TU>(int id) where TU : class, IStudentEntity;

    Task<Dictionary<Type, int>> GetProcessStepIdsByStudentId(int studentId, Type[] processTypes);



}

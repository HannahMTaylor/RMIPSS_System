using Microsoft.EntityFrameworkCore;
using RMIPSS_System.Data;
using RMIPSS_System.Repository.IRepository;
using System.Linq.Expressions;
using System.Reflection;
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

    public async Task Remove(T? entity)
    {
        if (entity != null) _dbSet.Remove(entity);
    }
    
    public async Task<T?> GetByIdAsync(int id)
    {
        return await _dbSet.FindAsync(id);
    }

    public void RemoveById(int id)
    {
        _dbSet.Remove(_dbSet.Find(id));
    }

    public async Task RemoveByIdAsync(int id)
    {
        var entity = await _dbSet.FindAsync(id);
        if (entity == null)
        {
            throw new InvalidOperationException($"Entity with ID {id} not found. Cannot delete.");
        }

        _dbSet.Remove(entity);
    }

    public async Task<T?> Save(T entity)
    {
        _db.Add(entity);
        _db.SaveChanges();
        return entity;
    }

    public async Task  Save()
    { 
        _db.SaveChanges();
    }
    
 public async Task<Dictionary<Type, int>> GetProcessStepIdsByStudentId(int studentId, Type[] processTypes)
{
    var ids = new Dictionary<Type, int>();

    // Create a list to store queries dynamically
    var queryTasks = new List<Task<KeyValuePair<Type, int>>>();

    foreach (var type in processTypes)
    {
        var dbSetMethod = _db.GetType()
            .GetMethods()
            .FirstOrDefault(m => m.Name == "Set" && m.GetParameters().Length == 0)?
            .MakeGenericMethod(type);

        if (dbSetMethod == null)
            throw new InvalidOperationException($"Could not find 'Set<{type.Name}>()' method in DbContext.");

        var dbSet = dbSetMethod.Invoke(_db, null);
        if (dbSet == null)
            throw new InvalidOperationException($"DbSet<{type.Name}> not found in DbContext.");

        var queryable = dbSet as IQueryable<object>;
        if (queryable == null)
            throw new InvalidOperationException($"DbSet<{type.Name}> could not be converted to IQueryable<object>.");

        // Get LINQ `Where` method dynamically
        var whereMethod = typeof(Queryable).GetMethods()
            .First(m => m.Name == "Where" && m.GetParameters().Length == 2)
            .MakeGenericMethod(type);

        var parameter = Expression.Parameter(type, "x");
        var property = Expression.Property(parameter, "StudentId");
        var constant = Expression.Constant(studentId);
        var lambda = Expression.Lambda(Expression.Equal(property, constant), parameter);

        var whereQuery = whereMethod.Invoke(null, new object[] { queryable, lambda });

        // Get `Select(x => x.Id)` dynamically
        var selectMethod = typeof(Queryable).GetMethods()
            .First(m => m.Name == "Select" && m.GetParameters().Length == 2)
            .MakeGenericMethod(type, typeof(int));

        var selectQuery = selectMethod.Invoke(null, new object[] { whereQuery, CreateSelectExpression(type) });

        // ✅ Use asynchronous execution in batch query
        var queryTask = await ExecuteQueryAsync(type, selectQuery);
        if (queryTask.Value > 0)
            ids[queryTask.Key] = queryTask.Value;
    }
    

    return ids;
}

// Helper method to execute FirstOrDefaultAsync dynamically
    private async Task<KeyValuePair<Type, int>> ExecuteQueryAsync(Type type, object selectQuery)
    {
        // Ensure `selectQuery` is properly cast to `IQueryable<int>`
        var queryable = selectQuery as IQueryable<int>;
        if (queryable == null)
        {
            throw new InvalidOperationException($"Query for {type.Name} could not be cast to IQueryable<int>.");
        }

        // ✅ Correctly locate `FirstOrDefaultAsync<T>()` for IQueryable<T>
        var firstOrDefaultAsyncMethod = typeof(EntityFrameworkQueryableExtensions)
            .GetMethods(BindingFlags.Public | BindingFlags.Static)
            .Where(m => m.Name == "FirstOrDefaultAsync" && m.IsGenericMethod)
            .FirstOrDefault(m =>
            {
                var parameters = m.GetParameters();
                return parameters.Length == 2 &&
                       parameters[0].ParameterType.GetGenericTypeDefinition() == typeof(IQueryable<>);
            })?
            .MakeGenericMethod(typeof(int));  // Use int as the generic type

        if (firstOrDefaultAsyncMethod == null)
        {
            throw new InvalidOperationException("Could not find FirstOrDefaultAsync<T>() method.");
        }

        // ✅ Execute the async query dynamically
        var task = (Task<int>)firstOrDefaultAsyncMethod.Invoke(null, new object[] { queryable, CancellationToken.None });

        int id = await task;
        return new KeyValuePair<Type, int>(type, id);
    }



// Helper method to dynamically generate Select Expression


    private static object CreateSelectExpression(Type processType)
    {
        var parameter = Expression.Parameter(processType, "x");
        var property = Expression.Property(parameter, "Id");
        return Expression.Lambda(property, parameter);
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

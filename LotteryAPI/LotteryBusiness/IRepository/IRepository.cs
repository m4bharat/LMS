using System.Data;
using System.Data.SqlClient;
using System.Linq.Expressions;

namespace LotteryAPI.LotteryBusiness.IRepository
{
    public interface IRepository<T> where T : class
    {
        T Get(int id);
        Task<T> GetAsync(int id);
        IEnumerable<T> GetAll();
        Task<IEnumerable<T>> GetAllAsync();
        IEnumerable<T> Find(Expression<Func<T, bool>> expression);
        Task<IEnumerable<T>> FindAsync(Expression<Func<T, bool>> expression);
        bool Exists(Expression<Func<T, bool>> expression);
        Task<bool> ExistsAsync(Expression<Func<T, bool>> expression);
        void Add(T entity);
        Task AddAsync(T entity);
        void Update(T entity);
        Task UpdateAsync(T entity);
        void Remove(T entity);
        Task RemoveAsync(T entity);
        void SaveChanges();
        Task SaveChangesAsync();
        void RemoveRange(List<T> entities);
        Task RemoveRangeAsync(List<T> entities);
        void AddRange(List<T> entities);
        Task AddRangeAsync(List<T> entities);

        DataSet GetDataSet(string commandtext, SqlParameter[] parameters, int? commandTimeout = null);
        int[] GenerateLotteryNumbers();
    }
}

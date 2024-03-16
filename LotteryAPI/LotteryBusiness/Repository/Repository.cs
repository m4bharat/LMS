using LotteryAPI.DbInfra.Model;
using LotteryAPI.LotteryBusiness.IRepository;
using Microsoft.EntityFrameworkCore;
using System.Data;
using System.Data.SqlClient;
using System.Linq.Expressions;

namespace LotteryAPI.LotteryBusiness.Repository
{
    public class Repository<T> : IRepository<T> where T : class
    {
        public readonly ContestDbContext _context;
        public ContestDbContext _Context { get { return this._context; } }

        public Repository(ContestDbContext ContestDbContext)
        {
            this._context = ContestDbContext;
        }
        public void Add(T entity)
        {
            this._context.Set<T>().Add(entity);
        }

        public async Task AddAsync(T entity)
        {
            await _context.Set<T>().AddAsync(entity);
        }

        public void AddRange(List<T> entities)
        {
            this._context.Set<T>().AddRange(entities);
        }

        public async Task AddRangeAsync(List<T> entities)
        {
            await _context.Set<T>().AddRangeAsync(entities);
        }

        public bool Exists(Expression<Func<T, bool>> expression)
        {
            return this._context.Set<T>().Any(expression);
        }

        public async Task<bool> ExistsAsync(Expression<Func<T, bool>> expression)
        {
            return await _context.Set<T>().AnyAsync(expression).ConfigureAwait(false);
        }

        public IEnumerable<T> Find(Expression<Func<T, bool>> expression)
        {
            return this._context.Set<T>().Where(expression).AsNoTracking().ToList();
        }

        public async Task<IEnumerable<T>> FindAsync(Expression<Func<T, bool>> expression)
        {
            return await _context.Set<T>().Where(expression).AsNoTracking().ToListAsync().ConfigureAwait(false);
        }

        public T Get(int id)
        {
            return this._context.Set<T>().Find(id);
        }
        public async Task<T> GetAsync(int id)
        {
            return await _context.Set<T>().FindAsync(id);
        }

        public IEnumerable<T> GetAll()
        {
            return this._context.Set<T>().AsNoTracking().ToList();
        }

        public async Task<IEnumerable<T>> GetAllAsync()
        {
            return await _context.Set<T>().AsNoTracking().ToListAsync().ConfigureAwait(false);
        }

        public void Remove(T entity)
        {
            this._context.Set<T>().Remove(entity);
        }

        public async Task RemoveAsync(T entity)
        {
            throw new NotImplementedException();
        }

        public void RemoveRange(List<T> entities)
        {
            this._context.Set<T>().RemoveRange(entities);
        }

        public Task RemoveRangeAsync(List<T> entities)
        {
            throw new NotImplementedException();
        }

        public void SaveChanges()
        {
            this._context.SaveChanges();
        }

        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync().ConfigureAwait(false);
        }

        public void Update(T entity)
        {
            this._context.Entry<T>(entity).State = EntityState.Modified;
        }

        public Task UpdateAsync(T entity)
        {
            throw new NotImplementedException();
        }

        public DataSet GetDataSet(string commandtext, SqlParameter[] parameters, int? commandTimeout = null)
        {
            //dbcontext.ChangeTracker();
            DataSet ds = new DataSet();
            using SqlCommand sqlCommand = (_context.Database.GetDbConnection() as SqlConnection).CreateCommand();
            sqlCommand.CommandTimeout = commandTimeout ?? sqlCommand.CommandTimeout;
            sqlCommand.CommandText = commandtext;
            if (parameters != null)
            {
                sqlCommand.Parameters.AddRange(parameters);
            }
            using SqlDataAdapter adp = new SqlDataAdapter(sqlCommand);
            adp.Fill(ds);


            return ds;
        }
        Random random = new Random();
        public int[] GenerateLotteryNumbers()
        {
            try
            {
                int[] numbers = new int[6]; // You can adjust the number of lottery numbers as needed
                HashSet<int> uniqueNumbers = new HashSet<int>();

                while (uniqueNumbers.Count < numbers.Length)
                {
                    int randomNumber = random.Next(1, 50); // You can adjust the range of lottery numbers as needed
                    uniqueNumbers.Add(randomNumber);
                }

                uniqueNumbers.CopyTo(numbers);

                return numbers;
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }
    }
}

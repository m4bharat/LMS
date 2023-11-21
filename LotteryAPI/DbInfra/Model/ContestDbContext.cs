using Microsoft.EntityFrameworkCore;

namespace LotteryAPI.DbInfra.Model
{
    public class ContestDbContext : DbContext
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        public ContestDbContext(DbContextOptions<ContestDbContext> dbContextOptions) : base(dbContextOptions)
        {

        }

        public ContestDbContext(DbContextOptions<ContestDbContext> dbContextOptions, IHttpContextAccessor httpContextAccessor) : base(dbContextOptions)
        {
            this._httpContextAccessor = httpContextAccessor;
        }
        public DbSet<ContestDetail> ContestDetails { get; set; }
        public DbSet<ContestResult> ContestResults { get; set; }
        public DbSet<LotteryNumbers> LotteryNumbers { get; set; }

        #region TrackableEntityPopulation
        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            AddTimestamps();
            return base.SaveChangesAsync(cancellationToken);
        }
        public override int SaveChanges()
        {
            AddTimestamps();
            return base.SaveChanges();
        }

        private void AddTimestamps()
        {
            var entities = ChangeTracker.Entries().Where(e => e.Entity is TrackableModel && (e.State == EntityState.Added || e.State == EntityState.Modified));
            var userName = _httpContextAccessor?.HttpContext?.User?.Claims.FirstOrDefault(x => x.Type == "preferred_username")?.Value;

            var currentUserName = !string.IsNullOrWhiteSpace(userName)
                ? userName
                : "Anonymous";

            foreach (var entity in entities)
            {
                if (entity.State == EntityState.Added)
                {
                    ((TrackableModel)entity.Entity).IsActive = true;
                    ((TrackableModel)entity.Entity).IsDeleted = false;
                    ((TrackableModel)entity.Entity).CreatedOn = DateTime.Now;
                    ((TrackableModel)entity.Entity).CreatedBy = currentUserName;
                    ((TrackableModel)entity.Entity).UpdatedOn = DateTime.Now;
                    ((TrackableModel)entity.Entity).UpdatedBy = currentUserName;
                }
                else
                {
                    ((TrackableModel)entity.Entity).UpdatedOn = DateTime.Now;
                    ((TrackableModel)entity.Entity).UpdatedBy = currentUserName;
                }
            }
        }

        #endregion
    }
}

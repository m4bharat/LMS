using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using LotteryAPI.DbInfra.Model;

namespace BackOffice.Data
{
    public class BackOfficeContext : DbContext
    {
        public BackOfficeContext (DbContextOptions<BackOfficeContext> options)
            : base(options)
        {
        }

        public DbSet<LotteryAPI.DbInfra.Model.ContestDetail> ContestDetails { get; set; } = default!;
    }
}

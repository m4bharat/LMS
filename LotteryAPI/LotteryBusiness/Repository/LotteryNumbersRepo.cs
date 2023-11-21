using LotteryAPI.DbInfra.Model;
using LotteryAPI.LotteryBusiness.IRepository;
using Microsoft.EntityFrameworkCore;

namespace LotteryAPI.LotteryBusiness.Repository
{
    public class LotteryNumbersRepo : Repository<LotteryNumbers>, ILotteryNumbersRepo
    {
        public LotteryNumbersRepo(ContestDbContext _context) : base(_context)
        {
        }

    }
}

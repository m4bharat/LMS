using LotteryAPI.DbInfra.Model;
using LotteryAPI.LotteryBusiness.IRepository;

namespace LotteryAPI.LotteryBusiness.Repository
{
    public class ContestDetailRepo : Repository<ContestDetail>, IContestDetailRepo
    {
        public ContestDetailRepo(ContestDbContext _context) : base(_context)
        {
        }
    }
}

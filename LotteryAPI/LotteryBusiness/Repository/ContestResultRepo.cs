using LotteryAPI.DbInfra.Model;
using LotteryAPI.LotteryBusiness.IRepository;

namespace LotteryAPI.LotteryBusiness.Repository
{
    public class ContestResultRepo : Repository<ContestResult>, IContestResultRepo
    {
        public ContestResultRepo(ContestDbContext _context) : base(_context)
        {
        }
    }
}

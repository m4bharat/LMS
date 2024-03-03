using LotteryAPI.DbInfra.Model;
using LotteryAPI.LotteryBusiness.DTOs;
using LotteryAPI.LotteryBusiness.IRepository;

namespace LotteryAPI.LotteryBusiness.Repository
{
    public class ContestResultRepo : Repository<ContestResult>, IContestResultRepo
    {
        public ContestResultRepo(ContestDbContext _context) : base(_context)
        {
        }



        public List<ContestResultResposeDto> getPublishedResultByContestId(int ContestId, string winningNumbers)
        {

            return _context.ContestResults.Join(_context.LotteryNumbers, c => c.LotteryNumberId, l => l.LotteryNumbersId, (c, l) => new { c, l })
                .Where(m => m.c.ContestDetailId == ContestId)
                .Select(k => new ContestResultResposeDto()
                {
                    ContestDetailId = k.c.ContestDetailId,
                    WinnerRank = k.c.ContestWinnerRank,
                    MatchCount = k.c.LotteryNumberMatchCount,
                    LotteryNumbersId = k.l.LotteryNumbersId,
                    WinnerNumber = winningNumbers,
                    TicketInArray = new int[] { k.l.LotteryNumber1, k.l.LotteryNumber2, k.l.LotteryNumber3, k.l.LotteryNumber4, k.l.LotteryNumber5, k.l.BonusNumber },
                    JoinLotteryNumber = string.Join(", ", new int[] { k.l.LotteryNumber1, k.l.LotteryNumber2, k.l.LotteryNumber3, k.l.LotteryNumber4, k.l.LotteryNumber5, k.l.BonusNumber })

                }).ToList();

        }
    }
}

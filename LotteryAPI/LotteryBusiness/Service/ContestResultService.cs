using LotteryAPI.DbInfra.Model;
using LotteryAPI.LotteryBusiness.DTOs;
using LotteryAPI.LotteryBusiness.IRepository;
using LotteryAPI.LotteryBusiness.IService;
using LotteryAPI.LotteryBusiness.Repository;

namespace LotteryAPI.LotteryBusiness.Service
{
    public class ContestResultService : IContestResultService
    {
        private readonly LotteryNumbersService _lotteryNumbersService;
        private readonly ContestResultRepo _ContestResultRepo;
        private readonly ContestDetailRepo _contestDetailRepo;
        public ContestResultService(ILotteryNumbersService lotteryNumbersService, IContestResultRepo ContestResultRepo, IContestDetailRepo contestDetailRepo)
        {

            _ContestResultRepo = (ContestResultRepo?)ContestResultRepo;
            _lotteryNumbersService = lotteryNumbersService as LotteryNumbersService;
            _contestDetailRepo = (ContestDetailRepo?)contestDetailRepo;
        }
        public async Task<List<ContestResultResposeDto>> GetLotteryResultAsync(int ContestId)
        {
            //if (await _contestDetailRepo.ExistsAsync(x => x.ContestDetailId == ContestId && x.IsResultPublished == true))
            //{

            //}
            //else
            //{
            return await getDrawResult(ContestId);
            //}
        }

        private async Task<List<ContestResultResposeDto>> getDrawResult(int ContestId)
        {
            List<LotteryNumbers> tickets = await _lotteryNumbersService.GetLotteryTicketListByContestId(ContestId);
            int[] winningNumbers = _ContestResultRepo.GenerateLotteryNumbers();
            List<ContestResultResposeDto> drawLotteryNumbersResposeList = new List<ContestResultResposeDto>();
            foreach (var t in tickets)
            {
                ContestResultResposeDto drawLotteryNumbersRespose = new ContestResultResposeDto();
                drawLotteryNumbersRespose.ContestDetailId = ContestId;
                drawLotteryNumbersRespose.LotteryNumbersId = t.LotteryNumbersId;
                drawLotteryNumbersRespose.WinnerNumber = string.Join(", ", winningNumbers);
                int[] numbers = { t.LotteryNumber1, t.LotteryNumber2, t.LotteryNumber3, t.LotteryNumber4, t.LotteryNumber5, t.BonusNumber };
                drawLotteryNumbersRespose.TicketInArray = numbers;
                drawLotteryNumbersRespose.JoinLotteryNumber = string.Join(", ", numbers);
                drawLotteryNumbersResposeList.Add(drawLotteryNumbersRespose);
            }

            foreach (var ticket in drawLotteryNumbersResposeList)
            {
                int matchingNumbers = CountMatchingNumbers(ticket.TicketInArray, winningNumbers);
                ticket.MatchCount = matchingNumbers;
                if (matchingNumbers > 0)
                {
                    var data = new ContestResult()
                    {
                        ContestDetailId = ticket.ContestDetailId,
                        ContestWinnerRank = ticket.WinnerRank,
                        LotteryNumberMatchCount = ticket.MatchCount,
                        LotteryNumberId = ticket.LotteryNumbersId,
                    };
                    await _ContestResultRepo.AddAsync(data);
                }
            }
            var contestDetailData = (await _contestDetailRepo.FindAsync(x => x.ContestDetailId == ContestId)).FirstOrDefault();
            contestDetailData.IsResultPublished = true;
            contestDetailData.DrawContestNumbers = string.Join(", ", winningNumbers);
            _contestDetailRepo.Update(contestDetailData);
            _ContestResultRepo.SaveChanges();
            _contestDetailRepo.SaveChanges();
            return drawLotteryNumbersResposeList;
        }

        private int CountMatchingNumbers(int[] userNumbers, int[] winningNumbers)
        {
            return userNumbers.Count(winningNumbers.Contains);
        }
    }
}

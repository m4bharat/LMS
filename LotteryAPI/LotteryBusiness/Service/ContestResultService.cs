using LotteryAPI.DbInfra.Model;
using LotteryAPI.LotteryBusiness.DTOs;
using LotteryAPI.LotteryBusiness.IRepository;
using LotteryAPI.LotteryBusiness.IService;
using LotteryAPI.LotteryBusiness.Repository;
using static System.Runtime.InteropServices.JavaScript.JSType;

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
            var data = _contestDetailRepo.Find(x => x.ContestDetailId == ContestId).FirstOrDefault();
            if (data == null) throw new InvalidDataException("Invalid Contest");
            if (data.IsResultPublished == true)
            {
                return getPublishedResultByContestId(ContestId, data.DrawContestNumbers);
            }
            else
            {
                return await getDrawResult(ContestId);
            }
        }
        private List<ContestResultResposeDto> getPublishedResultByContestId(int ContestId, string winningNumbers)
        {
            return _ContestResultRepo.getPublishedResultByContestId(ContestId, winningNumbers);
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
            var resultList = new List<ContestResult>();
            foreach (var ticket in drawLotteryNumbersResposeList)
            {
                int matchingNumbers = CountMatchingNumbers(ticket.TicketInArray, winningNumbers);
                ticket.MatchCount = matchingNumbers;
                if (DetermineWinningRank(matchingNumbers) <= 3)
                {
                    var data = new ContestResult()
                    {
                        ContestDetailId = ticket.ContestDetailId,
                        LotteryNumberMatchCount = ticket.MatchCount,
                        ContestWinnerRank = DetermineWinningRank(ticket.MatchCount),
                        LotteryNumberId = ticket.LotteryNumbersId,
                    };
                    resultList.Add(data);
                }
            }

            // check resultList have all three ranked winner
            if (foundAllCorrectWinner(resultList))
            {
                await _ContestResultRepo.AddRangeAsync(resultList);
            }
            else
            {
                await getDrawResult(ContestId);
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

        static int DetermineWinningRank(int matchingNumbers)
        {
            if (matchingNumbers == 5)
            {
                return 1;
            }
            else if (matchingNumbers >= 4)
            {
                return 2;
            }
            else if (matchingNumbers >= 3)
            {
                return 3;
            }
            return matchingNumbers + 100;
        }

        public static bool foundAllCorrectWinner(List<ContestResult> lis)
        {
            if (
                   lis.Any(x => x.ContestWinnerRank == 1)
                && lis.Any(x => x.ContestWinnerRank == 2)
                && lis.Any(x => x.ContestWinnerRank == 3)
                && lis.Where(x => x.ContestWinnerRank == 1).Count() == 1
                && lis.Where(x => x.ContestWinnerRank == 2).Count() == 1
                )
                return true;
            return false;
        }
    }
}

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

        public ContestResultService(ILotteryNumbersService lotteryNumbersService, IContestResultRepo ContestResultRepo)
        {
            
            _ContestResultRepo = (ContestResultRepo?)ContestResultRepo;
            _lotteryNumbersService = lotteryNumbersService as LotteryNumbersService;
        }
        public async Task<List<DrawLotteryNumbersRespose>> DrawLotteryNumbersAsync(int ContestId)
        {
            List<LotteryNumbers> tickets = await _lotteryNumbersService.GetLotteryTicketListByContestId(ContestId);
            int[] winningNumbers = _ContestResultRepo.GenerateLotteryNumbers();
            List<DrawLotteryNumbersRespose> drawLotteryNumbersResposeList = new List<DrawLotteryNumbersRespose>();
            foreach (var t in tickets)
            {
                DrawLotteryNumbersRespose drawLotteryNumbersRespose = new DrawLotteryNumbersRespose();
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
            }
            return drawLotteryNumbersResposeList;
        }

        private int CountMatchingNumbers(int[] userNumbers, int[] winningNumbers)
        {
            return userNumbers.Count(winningNumbers.Contains);
        }
    }
}

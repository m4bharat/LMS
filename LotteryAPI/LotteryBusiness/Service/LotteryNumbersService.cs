using LotteryAPI.DbInfra.Model;
using LotteryAPI.LotteryBusiness.DTOs;
using LotteryAPI.LotteryBusiness.IRepository;
using LotteryAPI.LotteryBusiness.IService;
using LotteryAPI.LotteryBusiness.Repository;
using System.Linq;

namespace LotteryAPI.LotteryBusiness.Service
{
    public class LotteryNumbersService : ILotteryNumbersService
    {
        private readonly LotteryNumbersRepo _lotteryNumbersRepo;

        public LotteryNumbersService(ILotteryNumbersRepo lotteryNumbersRepo)
        {
            _lotteryNumbersRepo = lotteryNumbersRepo as LotteryNumbersRepo;
        }

        public async Task<LotteryNumbers> GenerateLotteryTicket(CreateLotteryRequestDto param)
        {
            LotteryNumbers lotteryNumbers = new LotteryNumbers();
            try
            {

                int[] tickets = _lotteryNumbersRepo.GenerateLotteryNumbers();
                lotteryNumbers.LotteryNumber1 = tickets[0];
                lotteryNumbers.LotteryNumber2 = tickets[1];
                lotteryNumbers.LotteryNumber3 = tickets[2];
                lotteryNumbers.LotteryNumber4 = tickets[3];
                lotteryNumbers.LotteryNumber5 = tickets[4];
                lotteryNumbers.BonusNumber = tickets[5];
                lotteryNumbers.ContestDetailId = param.ContestDetailId;
                lotteryNumbers.PaymentTransactionId = param.PaymentTransactionId;
                lotteryNumbers.UserId = param.UserId;
                _lotteryNumbersRepo.Add(lotteryNumbers);
                await _lotteryNumbersRepo.SaveChangesAsync();

            }
            catch (Exception ex)
            {

            }
            return lotteryNumbers;
        }

        public Task<LotteryNumbers> GetLotteryTicketById(int Id)
        {
            throw new NotImplementedException();
        }

        public async Task<List<LotteryNumbers>> GetLotteryTicketListByContestId(int ContestId)
        {
            return (await _lotteryNumbersRepo.FindAsync(x => x.ContestDetailId == ContestId && x.IsDeleted == false && x.IsActive == true)).ToList();
        }

    }
}

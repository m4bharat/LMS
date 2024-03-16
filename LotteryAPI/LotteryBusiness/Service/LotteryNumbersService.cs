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
        private readonly ContestDetailRepo _contestDetailRepo;
        public LotteryNumbersService(ILotteryNumbersRepo lotteryNumbersRepo, IContestDetailRepo contestDetailRepo)
        {
            _lotteryNumbersRepo = lotteryNumbersRepo as LotteryNumbersRepo;
            _contestDetailRepo = contestDetailRepo as ContestDetailRepo;
        }

        public async Task<BuyLotteryResponseDto> BuyLotteryTicketAsync(CreateLotteryRequestDto param)
        {
            BuyLotteryResponseDto resp = new BuyLotteryResponseDto();
            try
            {
                //TO DO----------
                //CHeck expiry for contest
                foreach (var l in param.PurchasedTickets)
                {
                    var contestdata = _contestDetailRepo.Find(x => x.ContestDetailId == l.ContestDetailId).FirstOrDefault();
                    if (contestdata != null)
                    {
                        int[] tickets = _lotteryNumbersRepo.GenerateLotteryNumbers();
                        var lotteryNumbers = new LotteryNumbers();
                        lotteryNumbers.LotteryNumber1 = tickets[0];
                        lotteryNumbers.LotteryNumber2 = tickets[1];
                        lotteryNumbers.LotteryNumber3 = tickets[2];
                        lotteryNumbers.LotteryNumber4 = tickets[3];
                        lotteryNumbers.LotteryNumber5 = tickets[4];
                        lotteryNumbers.BonusNumber = tickets[5];
                        lotteryNumbers.ContestDetailId = l.ContestDetailId;
                        lotteryNumbers.PaymentTransactionId = param.PaymentTransactionId;
                        lotteryNumbers.UserId = param.UserId;

                        await _lotteryNumbersRepo.AddAsync(lotteryNumbers);
                        contestdata.ContestTotalBoughtTicket = contestdata.ContestTotalBoughtTicket + 1;
                        _contestDetailRepo.Update(contestdata);

                        PurchasedTicketsDetail d = new PurchasedTicketsDetail();
                        d.LotteryNumber1 = lotteryNumbers.LotteryNumber1;
                        d.LotteryNumber2 = lotteryNumbers.LotteryNumber2;
                        d.LotteryNumber3 = lotteryNumbers.LotteryNumber3;
                        d.LotteryNumber4 = lotteryNumbers.LotteryNumber4;
                        d.LotteryNumber5 = lotteryNumbers.LotteryNumber5;
                        d.BonusNumber = lotteryNumbers.BonusNumber;
                        d.ContestDetailId = lotteryNumbers.ContestDetailId;
                        resp.PurchasedTicketsDetail.Add(d);
                    }
                    else
                    {
                        throw new InvalidDataException("Contest doesn't exist or not live/expired.");
                    }
                }

                // Saving and Updating tables
                await _lotteryNumbersRepo.SaveChangesAsync();
                await _contestDetailRepo.SaveChangesAsync();
                //resp.UserId = param.UserId;
                resp.PaymentTransactionId = param.PaymentTransactionId;
                return resp;
            }

            catch (Exception ex)
            {
                throw ex;
            }

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

using LotteryAPI.DbInfra.Model;
using LotteryAPI.LotteryBusiness.DTOs;
using LotteryAPI.LotteryBusiness.IRepository;
using LotteryAPI.LotteryBusiness.IService;
using LotteryAPI.LotteryBusiness.Repository;

namespace LotteryAPI.LotteryBusiness.Service
{
    public class ContestDetailService : IContestDetailService
    {
        private readonly ContestDetailRepo _contestDetailRepo;

        public ContestDetailService(IContestDetailRepo contestDetailRepo)
        {
            _contestDetailRepo = (ContestDetailRepo?)contestDetailRepo;
        }
        public async Task<bool> CreateContestAsync(CreateContestRequestDto param)
        {

            try
            {
                var data = new ContestDetail()
                {
                    ContestDetailId = param.ContestDetailId,
                    ContestDetailBannerImgUrl = param.ContestDetailBannerImgUrl,
                    ContestDetailTileImgUrl = param.ContestDetailTileImgUrl,
                    ContestDetailTitle = param.ContestDetailTitle,
                    ContestDetailDescription = param.ContestDetailDescription,
                    ContestTicketPrice = param.ContestTicketPrice,
                    ContestTotalTicket = param.ContestTotalTicket,
                    ContestTotalBoughtTicket = param.ContestTotalBoughtTicket,
                    ContestValue = param.ContestTicketPrice * param.ContestTotalTicket,
                    ContestStartDate = param.ContestStartDate,
                    ContestStartTime = param.ContestStartTime,
                    ContestEndDate = param.ContestEndDate,
                    ContestEndTime = param.ContestEndTime,
                    ContestDrawDate = param.ContestDrawDate,
                    ContestDrawTime = param.ContestDrawTime,
                    ContestNumberOfWinners = param.ContestNumberOfWinners,
                    IsResultPublished = false,
                    DrawContestNumbers = param.DrawContestNumbers

                };
                await _contestDetailRepo.AddAsync(data);
                await _contestDetailRepo.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }


        }

    }
}

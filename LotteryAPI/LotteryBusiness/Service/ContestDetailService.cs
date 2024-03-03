using LotteryAPI.DbInfra.Model;
using LotteryAPI.LotteryBusiness.DTOs;
using LotteryAPI.LotteryBusiness.IRepository;
using LotteryAPI.LotteryBusiness.IService;
using LotteryAPI.LotteryBusiness.Repository;
using System.Collections.Generic;

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

        public async Task<List<ContestDetailResponseDto>> GetLiveContestListAsync()
        {
            var data = await _contestDetailRepo.FindAsync(x => x.IsActive);
            return EntityToDto(data.ToList());
        }

        private List<ContestDetailResponseDto> EntityToDto(List<ContestDetail> data)
        {
            List<ContestDetailResponseDto> lst = new List<ContestDetailResponseDto>();
            foreach (var d in data)
            {
                lst.Add(new ContestDetailResponseDto()
                {
                    ContestDetailId = d.ContestDetailId,
                    ContestDetailBannerImgUrl = d.ContestDetailBannerImgUrl,
                    ContestDetailTileImgUrl = d.ContestDetailTileImgUrl,
                    ContestDetailTitle = d.ContestDetailTitle,
                    ContestDetailDescription = d.ContestDetailDescription,
                    ContestTicketPrice = d.ContestTicketPrice,
                    ContestTotalTicket = d.ContestTotalTicket,
                    ContestTotalBoughtTicket = d.ContestTotalBoughtTicket,
                    ContestValue = d.ContestValue,
                    ContestStartDate = d.ContestStartDate,
                    ContestStartTime = d.ContestStartTime,
                    ContestEndDate = d.ContestEndDate,
                    ContestEndTime = d.ContestEndTime,
                    ContestDrawDate = d.ContestDrawDate,
                    ContestDrawTime = d.ContestDrawTime,
                    ContestNumberOfWinners = d.ContestNumberOfWinners,
                    //IsResultPublished = d.IsResultPublished,
                    DrawContestNumbers = d.DrawContestNumbers,
                });

            }
            return lst;
        }
    }
}

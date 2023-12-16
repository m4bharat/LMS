using LotteryAPI.LotteryBusiness.DTOs;

namespace LotteryAPI.LotteryBusiness.IService
{
    public interface IContestDetailService
    {
        public Task<bool> CreateContestAsync(CreateContestRequestDto param);
    }
}

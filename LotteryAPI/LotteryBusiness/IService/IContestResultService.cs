using LotteryAPI.LotteryBusiness.DTOs;

namespace LotteryAPI.LotteryBusiness.IService
{
    public interface IContestResultService
    {
        public Task<List<ContestResultResposeDto>> GetLotteryResultAsync(int ContestId);
    }
}

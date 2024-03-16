using LotteryAPI.LotteryBusiness.DTOs;

namespace LotteryAPI.LotteryBusiness.IService
{
    public interface IContestResultService
    {
        Task<List<ContestResultResposeDto>> GetLotteryResultAsync(int ContestId);
        Task<List<ContestResultResposeDto>> GetAllPublishedResultAsync(int recordCount);
    }
}

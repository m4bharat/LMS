using LotteryAPI.DbInfra.Model;
using LotteryAPI.LotteryBusiness.DTOs;

namespace LotteryAPI.LotteryBusiness.IRepository
{
    public interface IContestResultRepo : IRepository<ContestResult>
    {
        List<ContestResultResposeDto> getPublishedResultByContestId(int ContestId, string winningNumbers);
        List<ContestResultResposeDto> getAllPublishedResult(int recordCount);
    }
}

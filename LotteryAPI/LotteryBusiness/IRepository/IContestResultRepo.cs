using LotteryAPI.DbInfra.Model;
using LotteryAPI.LotteryBusiness.DTOs;

namespace LotteryAPI.LotteryBusiness.IRepository
{
    public interface IContestResultRepo : IRepository<ContestResult>
    {
        List<ContestResultResposeDto> getPublishedResult(int ContestId, string winningNumbers);
    }
}

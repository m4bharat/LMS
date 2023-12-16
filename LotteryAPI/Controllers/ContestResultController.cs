using LotteryAPI.LotteryBusiness.DTOs;
using LotteryAPI.LotteryBusiness.IService;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace LotteryAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ContestResultController : ControllerBase
    {
        private readonly IContestResultService _ContestResultService;

        public ContestResultController(IContestResultService ContestResultService)
        {
            _ContestResultService = ContestResultService;
        }
        // GET: api/<ContestResultController>
        [HttpGet]
        public async Task<List<ContestResultResposeDto>> GetLotteryResult(int CampaignId)
        {
            return await _ContestResultService.GetLotteryResultAsync(CampaignId);
        }


    }
}

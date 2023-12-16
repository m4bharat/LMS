using LotteryAPI.DbInfra.Model;
using LotteryAPI.LotteryBusiness.DTOs;
using LotteryAPI.LotteryBusiness.IService;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace LotteryAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ContestDetailController : ControllerBase
    {
        private readonly IContestDetailService _ContestDetailService;

        public ContestDetailController(IContestDetailService ContestDetailService)
        {
            _ContestDetailService = ContestDetailService;
        }
        // POST: api/<LotteryTicketController>
        [HttpPost]
        public async Task<bool> CreateAsync(CreateContestRequestDto req)
        {
            return await _ContestDetailService.CreateContestAsync(req);
        }
    }
}

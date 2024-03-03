using LotteryAPI.DbInfra.Model;
using LotteryAPI.LotteryBusiness.DTOs;
using LotteryAPI.LotteryBusiness.IService;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using UserIdentity.Service.Authorization;

namespace LotteryAPI.Controllers
{
    [Authorize]
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

        [AllowAnonymous]
        [HttpGet]
        public async Task<List<ContestDetailResponseDto>> GetContestList()
        {
            return await _ContestDetailService.GetLiveContestListAsync();
        }
    }
}

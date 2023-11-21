using LotteryAPI.DbInfra.Model;
using LotteryAPI.LotteryBusiness.DTOs;
using LotteryAPI.LotteryBusiness.IService;
using LotteryAPI.LotteryBusiness.Service;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace LotteryAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LotteryTicketController : ControllerBase
    {
        private readonly ILotteryNumbersService _LotteryNumbersService;

        public LotteryTicketController(ILotteryNumbersService LotteryNumbersService)
        {
            _LotteryNumbersService = LotteryNumbersService;
        }
        // POST: api/<LotteryTicketController>
        [HttpPost]
        public async Task<LotteryNumbers> GetAsync(CreateLotteryRequestDto req)
        {
            return await _LotteryNumbersService.GenerateLotteryTicket(req);
        }
    }
}
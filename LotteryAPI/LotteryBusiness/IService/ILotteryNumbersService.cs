﻿using LotteryAPI.DbInfra.Model;
using LotteryAPI.LotteryBusiness.DTOs;

namespace LotteryAPI.LotteryBusiness.IService
{
    public interface ILotteryNumbersService
    {
        public Task<LotteryNumbers> GenerateLotteryTicket(CreateLotteryRequestDto param);
        public Task<List<LotteryNumbers>> GetLotteryTicketListByContestId(int ContestId);
        public Task<LotteryNumbers> GetLotteryTicketById(int Id);
    }
}

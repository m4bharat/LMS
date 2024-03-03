using LotteryAPI.DbInfra.Model;
using UserIdentity.Service.Entities;

namespace LotteryAPI.LotteryBusiness.DTOs
{
    public class CreateLotteryRequestDto
    {
        public int UserId { get; set; }
        public string PaymentTransactionId { get; set; }
        public List<PurchasedTickets> PurchasedTickets { get; set; }
    }

    public class PurchasedTickets
    {
        public int ContestDetailId { get; set; }
        public int Qty { get; set; }
    }

    public class BuyLotteryResponseDto
    {
        public User UserId { get; set; }
        public string PaymentTransactionId { get; set; }
        public List<PurchasedTicketsDetail> PurchasedTicketsDetail { get; set; }

    }

    public class PurchasedTicketsDetail
    {
        public int ContestDetailId { get; set; }
        public int LotteryNumberId { get; set; }
        public int LotteryNumber1 { get; set; }
        public int LotteryNumber2 { get; set; }
        public int LotteryNumber3 { get; set; }
        public int LotteryNumber4 { get; set; }
        public int LotteryNumber5 { get; set; }
        public int BonusNumber { get; set; }
    }

}

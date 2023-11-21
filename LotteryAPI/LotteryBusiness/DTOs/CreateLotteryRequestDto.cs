namespace LotteryAPI.LotteryBusiness.DTOs
{
    public class CreateLotteryRequestDto
    {
        public int ContestDetailId { get; set; }
        public int UserId { get; set; }
        public string PaymentTransactionId { get; set; }
    }
}

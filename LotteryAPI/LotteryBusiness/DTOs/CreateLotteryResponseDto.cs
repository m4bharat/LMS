namespace LotteryAPI.LotteryBusiness.DTOs
{
    public class CreateLotteryResponseDto
    {
        public string Name { get; set; }
        public string UserId { get; set; }
        public string PhoneNumber { get; set; }
        public string EmailAddress { get; set; }
        public string PaymentTransactionId { get; set; }
        public string LotteryNumber { get; set; }
        public DateTime PurchaseDate { get;}
    }
}

namespace LotteryAPI.DbInfra.Model
{
    public class LotteryNumbers : TrackableModel
    {
        public int LotteryNumbersId { get; set; }
        public int ContestDetailId { get; set; }
        public int UserId { get; set; }
        public string PaymentTransactionId { get; set; }
        public int LotteryNumber1 { get; set; }
        public int LotteryNumber2 { get; set; }
        public int LotteryNumber3 { get; set; }
        public int LotteryNumber4 { get; set; }
        public int LotteryNumber5 { get; set; }
        public int BonusNumber { get; set; }

    }
}

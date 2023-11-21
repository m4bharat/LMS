namespace LotteryAPI.LotteryBusiness.DTOs
{
    public class DrawLotteryNumbersRespose
    {
        public int LotteryNumbersId { get; set; }
        public int ContestDetailId { get; set; }
        public string JoinLotteryNumber { get; set; }
        public string WinnerNumber { get; set; }
        public int MatchCount { get; set; }
        public int WinnerRank { get; set; }
        public int[] TicketInArray { get; set; }
    }
}

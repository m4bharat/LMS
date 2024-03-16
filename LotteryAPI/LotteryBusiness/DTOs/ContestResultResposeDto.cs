namespace LotteryAPI.LotteryBusiness.DTOs
{
    public class ContestResultResposeDto
    {
        public int LotteryNumbersId { get; set; }
        public int ContestDetailId { get; set; }
        public string JoinLotteryNumber { get; set; }
        public string WinnerNumber { get; set; }
        public int MatchCount { get; set; }
        public int WinnerRank { get; set; }
        public int[] TicketInArray { get; set; }
    }

    public class ContestResultDto
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Username { get; set; }
        public string ContestDetailId { get; set; }
        public string LotteryNumberId { get; set; }
        public string ContestDetailTitle { get; set; }
        public string ContestDrawDate { get; set; }
        public string LotteryNumber { get; set; }
        public string[] LotteryNumberInArray { get; set; }
        public string DrawContestNumbers { get; set; }
        public string[] DrawContestNumbersInArray { get; set; }
        public string ContestWinnerRank { get; set; }
        public string LotteryNumberMatchCount { get; set; }
    }
}

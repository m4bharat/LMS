namespace LotteryAPI.DbInfra.Model
{
    public class ContestResult : TrackableModel
    {
        public int ContestResultId { get; set; }
        public int ContestDetailId { get; set; }
        public int ContestWinnerRank { get; set; }
        public int LotteryNumberMatchCount { get; set; }
        public int LotteryNumberId { get; set; }
    }
}

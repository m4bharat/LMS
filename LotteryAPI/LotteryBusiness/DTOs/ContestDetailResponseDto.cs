namespace LotteryAPI.LotteryBusiness.DTOs
{
    public class ContestDetailResponseDto
    {
        public int ContestDetailId { get; set; }
        public string ContestDetailBannerImgUrl { get; set; }
        public string ContestDetailTileImgUrl { get; set; }
        public string ContestDetailTitle { get; set; }
        public string ContestDetailDescription { get; set; }
        public int ContestTicketPrice { get; set; }
        public int ContestTotalTicket { get; set; }
        public int ContestTotalBoughtTicket { get; set; }
        public int ContestValue { get; set; }
        public DateTime ContestStartDate { get; set; }
        public DateTime ContestStartTime { get; set; }
        public DateTime ContestEndDate { get; set; }
        public DateTime ContestEndTime { get; set; }
        public DateTime ContestDrawDate { get; set; }
        public DateTime ContestDrawTime { get; set; }
        public int ContestNumberOfWinners { get; set; }
        public string DrawContestNumbers { get; set; }
    }
}

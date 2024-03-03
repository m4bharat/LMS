using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LotteryAPI.Migrations
{
    /// <inheritdoc />
    public partial class Initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ContestDetails",
                columns: table => new
                {
                    ContestDetailId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ContestDetailBannerImgUrl = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ContestDetailTileImgUrl = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ContestDetailTitle = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ContestDetailDescription = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ContestTicketPrice = table.Column<int>(type: "int", nullable: false),
                    ContestTotalTicket = table.Column<int>(type: "int", nullable: false),
                    ContestTotalBoughtTicket = table.Column<int>(type: "int", nullable: false),
                    ContestValue = table.Column<int>(type: "int", nullable: false),
                    ContestStartDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ContestStartTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ContestEndDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ContestEndTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ContestDrawDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ContestDrawTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ContestNumberOfWinners = table.Column<int>(type: "int", nullable: false),
                    IsResultPublished = table.Column<bool>(type: "bit", nullable: false),
                    DrawContestNumbers = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    UpdatedOn = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedBy = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ContestDetails", x => x.ContestDetailId);
                });

            migrationBuilder.CreateTable(
                name: "ContestResults",
                columns: table => new
                {
                    ContestResultId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ContestDetailId = table.Column<int>(type: "int", nullable: false),
                    ContestWinnerRank = table.Column<int>(type: "int", nullable: false),
                    LotteryNumberMatchCount = table.Column<int>(type: "int", nullable: false),
                    LotteryNumberId = table.Column<int>(type: "int", nullable: false),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    UpdatedOn = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedBy = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ContestResults", x => x.ContestResultId);
                });

            migrationBuilder.CreateTable(
                name: "LotteryNumbers",
                columns: table => new
                {
                    LotteryNumbersId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ContestDetailId = table.Column<int>(type: "int", nullable: false),
                    UserId = table.Column<int>(type: "int", nullable: false),
                    PaymentTransactionId = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    LotteryNumber1 = table.Column<int>(type: "int", nullable: false),
                    LotteryNumber2 = table.Column<int>(type: "int", nullable: false),
                    LotteryNumber3 = table.Column<int>(type: "int", nullable: false),
                    LotteryNumber4 = table.Column<int>(type: "int", nullable: false),
                    LotteryNumber5 = table.Column<int>(type: "int", nullable: false),
                    BonusNumber = table.Column<int>(type: "int", nullable: false),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    UpdatedOn = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedBy = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LotteryNumbers", x => x.LotteryNumbersId);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ContestDetails");

            migrationBuilder.DropTable(
                name: "ContestResults");

            migrationBuilder.DropTable(
                name: "LotteryNumbers");
        }
    }
}

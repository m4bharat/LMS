
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE SP_GetPublishedResults
	@isActiveOnly bit=0,
	@ContestDetailId int=null
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

        SELECT U.FirstName,U.LastName,U.Username, CD.ContestDetailId,CD.ContestDetailTitle,CD.ContestDrawDate,
		(CONVERT(VARCHAR,LN.LotteryNumber1)+','+CONVERT(VARCHAR,LN.LotteryNumber2)+','+CONVERT(VARCHAR,LN.LotteryNumber3)+','+
		CONVERT(VARCHAR,LN.LotteryNumber4)+','+CONVERT(VARCHAR,LN.LotteryNumber5)+','+CONVERT(VARCHAR,LN.BonusNumber)) AS 'LotteryNumber',
		CD.DrawContestNumbers,CR.ContestWinnerRank,CR.LotteryNumberMatchCount
		FROM ContestResults  CR
		JOIN ContestDetails CD ON CR.ContestDetailId=CD.ContestDetailId
		JOIN LotteryNumbers LN ON CR.ContestDetailId=LN.ContestDetailId AND CR.LotteryNumberId=LN.LotteryNumbersId
		JOIN Users U on LN.UserId=U.Id
		WHERE CD.IsResultPublished=1 and CD.IsActive=1 ORDER BY CR.ContestWinnerRank ASC
END
GO

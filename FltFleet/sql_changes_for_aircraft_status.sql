-- Copyright (c) 2026 CODE Éxitos. All rights reserved.
-- Athanor Workshop License 1.0 - see LICENSE at the repository root. Not open source.
USE [flt]
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[AircraftStatus](
	[AircraftStatusId] [int] IDENTITY(1,1) NOT NULL,
	[AircraftId] [int] NOT NULL,
	[Status] [int] NOT NULL,
	[EngineerComment] [nvarchar](2048) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[HangarComment] [nvarchar](2048) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[UTCDateModified] [datetime] NOT NULL,
 CONSTRAINT [PK_AircraftStatus] PRIMARY KEY CLUSTERED 
(
	[AircraftStatusId] ASC
)WITH (IGNORE_DUP_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]

GO

USE [flt]
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[fetchAircraftStatus]
	@MSN varchar(128)
AS
BEGIN
	SET NOCOUNT ON;

	SELECT loc.LocationName, v.MSN, vs.Status,vs.EngineerComment, vs.HangarComment, vs.UTCDateModified 
	FROM Aircraft v
	left outer join AircraftStatus vs 
	ON v.AircraftId = vs.AircraftId
	left outer join Location loc
	ON v.CurrentLocationId = loc.LocactionId
	WHERE v.IsDeleted = 0 AND 
		v.MSN = @MSN

END

GO

USE [flt]
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO


CREATE PROCEDURE [dbo].[listAircraftStatuses]
	@ProgId Int = 0,
	@SortBy varchar(32)
AS
BEGIN
	SET NOCOUNT ON;

	if @SortBy = 'Location' 
	begin
		SELECT loc.LocationName, v.MSN, vs.Status, vs.EngineerComment, vs.HangarComment, vs.UTCDateModified 
		FROM Aircraft v
		left outer join AircraftStatus vs 
		ON v.AircraftId = vs.AircraftId
		left outer join Location loc
		ON v.CurrentLocationId = loc.LocactionId
		WHERE v.IsDeleted = 0 AND 
			((@ProgId = 0) OR (v.ProgId = @ProgId))
		ORDER BY loc.LocationName DESC, v.MSN
	end 

	if @SortBy = 'MSN' 
	begin
		SELECT loc.LocationName, v.MSN, vs.Status, vs.EngineerComment, vs.HangarComment, vs.UTCDateModified 
		FROM Aircraft v
		left outer join AircraftStatus vs 
		ON v.AircraftId = vs.AircraftId
		left outer join Location loc
		ON v.CurrentLocationId = loc.LocactionId
		WHERE v.IsDeleted = 0 AND 
			((@ProgId = 0) OR (v.ProgId = @ProgId))
		ORDER BY v.MSN
	end 

	if @SortBy = 'Date' 
	begin
		SELECT loc.LocationName, v.MSN, vs.Status, vs.EngineerComment, vs.HangarComment, vs.UTCDateModified 
		FROM Aircraft v
		left outer join AircraftStatus vs 
		ON v.AircraftId = vs.AircraftId
		left outer join Location loc
		ON v.CurrentLocationId = loc.LocactionId
		WHERE v.IsDeleted = 0 AND 
			((@ProgId = 0) OR (v.ProgId = @ProgId))
		ORDER BY vs.UTCDateModified DESC, v.MSN
	end 
	
END


GO
USE [flt]
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO


CREATE PROCEDURE [dbo].[updateAircraftStatus]
	@MSN varchar(128),
	@Status int, 
	@EngineerComment varchar(128),
	@HangarComment varchar(128)
AS
BEGIN

	declare @AircraftID int
	select @AircraftID = AircraftID from Aircraft where MSN = @MSN

	if not exists(Select * from [AircraftStatus] where AircraftID = @AircraftID) 
	begin
		insert into AircraftStatus (AircraftID, Status, EngineerComment, HangarComment, UTCDateModified)
		values (@AircraftID, 0, '', '',  GetUTCDate())
	end 

	UPDATE [dbo].[AircraftStatus]
	SET [Status] = @Status,
		[EngineerComment] = @EngineerComment,
		[HangarComment] = @HangarComment,
		[UTCDateModified] =  GetUTCDate()
	WHERE AircraftID = @AircraftID
	
END


GO

-- Copyright (c) 2026 CODE Éxitos. All rights reserved.
-- Athanor Workshop License 1.0 - see LICENSE at the repository root. Not open source.



USE FLT
go

ALTER TABLE Flight
ADD DuplicateCount INTEGER NOT NULL DEFAULT (0)
go

CREATE PROCEDURE zFix_Set_Flight_DuplicateCount
AS
BEGIN
	DECLARE @AircraftId INTEGER
	DECLARE @LocationId INTEGER
	DECLARE @FlightDateSort VARCHAR(10)
	DECLARE @WorkShiftIndicatorId INTEGER
	DECLARE @FlightId INTEGER
	DECLARE @DuplicateCount INTEGER
	DECLARE @Count INTEGER

	DECLARE curFlights1 CURSOR FOR 
		SELECT 
			AircraftId, 
			LocationId, 
			FlightDateSort, 
			WorkShiftIndicatorId
		FROM 
			Flight 
		GROUP BY 
			AircraftId, LocationId, FlightDateSort, WorkShiftIndicatorId
		HAVING 
			COUNT(*) > 1
		ORDER BY 
			AircraftId, LocationId, FlightDateSort, WorkShiftIndicatorId

	OPEN curFlights1
	FETCH NEXT FROM curFlights1 INTO @AircraftId, @LocationId, @FlightDateSort, @WorkShiftIndicatorId
	WHILE @@FETCH_STATUS = 0
	BEGIN
	
		SELECT @Count = 0

		DECLARE curFlights2 CURSOR FOR
			SELECT 
				FlightId,
				DuplicateCount
			FROM 
				Flight
			WHERE
				AircraftId = @AircraftId AND
				LocationId = @LocationId AND
				FlightDateSort = @FlightDateSort AND
				WorkShiftIndicatorId = @WorkShiftIndicatorId
			ORDER BY FlightId
			FOR UPDATE OF DuplicateCount

		OPEN curFlights2
		FETCH NEXT FROM curFlights2 INTO @FlightId, @DuplicateCount
		WHILE @@FETCH_STATUS = 0
		BEGIN
			UPDATE Flight SET DuplicateCount = @Count WHERE CURRENT OF curFlights2
			SELECT @COUNT = @COUNT + 1

			FETCH NEXT FROM curFlights2 INTO @FlightId, @DuplicateCount
		END
		CLOSE curFlights2
		DEALLOCATE curFlights2

		FETCH NEXT FROM curFlights INTO @AircraftId, @LocationId, @FlightDateSort, @WorkShiftIndicatorId
	END
	CLOSE curFlights1
	DEALLOCATE curFlights1
END
GO

EXECUTE zFix_Set_Flight_DuplicateCount()
GO

ALTER TABLE ADD CONSTRAINT [Flight_Unique] UNIQUE ([AircraftId], [LocationId], [FlightDateSort], [WorkShiftIndicatorId])
GO
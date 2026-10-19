// Copyright (c) 2026 CODE Éxitos. All rights reserved.
// Athanor Workshop License 1.0 - see LICENSE at the repository root. Not open source.
using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace FltRestService.Model
{
    public class Flight
    {
        public int FlightId { get; set; }

        public int AircraftId { get; set; }
        public int LocationId { get; set; }

        public int EvaluatorId { get; set; }
        public int SchedRouteId { get; set; }
        public int FlownRouteId { get; set; }
        public string FlightDate { get; set; }
        public int WorkShiftIndicatorId { get; set; }
        
        public int AirframeId { get; set; }
        public int Cargo1Id { get; set; }
        public int Cargo2Id { get; set; }
        public int BallastId { get; set; }

        public short PreFlightFtiOK { get; set; }
        public short PreFlightTelemetryOK { get; set; }
        public short PreFlightTelemetryCheckComplete { get; set; }
        public short OilLevelIndicatorStart { get; set; }
        public short HydraulicLevelIndicatorStart { get; set; }

        public string DepartureTime { get; set; }
        public string ReturnTime { get; set; }

        public decimal EngineHoursStart { get; set; }
        public decimal EngineHoursEnd { get; set; }

        public decimal HobbsStart { get; set; }
        public decimal HobbsEnd { get; set; }
        public int MeterUnitId { get; set; }
        public decimal UsedMeterOffset { get; set; }
        
        public short PostFlightFtiOK { get; set; }
        public short PostFlightTelemetryCheckComplete { get; set; }
        public short NacelleCheckBuildUp { get; set; }
        public short NacelleCheckDiscoloration { get; set; }

        public LiquidFills LiquidFills { get; set; }
        public FlightComments FlightComments { get; set; }
        public EngineRuns EngineRuns { get; set; }
        public RegionTimes RegionTimes { get; set; }

        public Flight()
        {
            this.LiquidFills = new LiquidFills();
            this.FlightComments = new FlightComments();
            this.EngineRuns = new EngineRuns();
            this.RegionTimes = new RegionTimes();
        }
    }

    [CollectionDataContract]
    public class Flights : List<Flight>
    {
        public Flights() { }
        public Flights(List<Flight> Flights) : base(Flights) { }
    }

    #region "LiquidFills"
    public class LiquidFill
    {
        public int LiquidFillId { get; set; }
        public int LiquidFillTypeId { get; set; }
        public int LiquidFillSubTypeId { get; set; }
        public int LiquidUnitId { get; set; }
        public decimal LiquidAmt { get; set; }
        public decimal HobbsReading { get; set; }
        public string LiquidFillLocation { get; set; }
        public string LiquidFillApprover { get; set; }
    }

    [CollectionDataContract]
    public class LiquidFills : List<LiquidFill>
    {
        public LiquidFills() { }
        public LiquidFills(List<LiquidFill> LiquidFills) : base(LiquidFills) { }
    }
    #endregion

    #region "FlightComments"
    public class FlightComment
    {
        public int FlightCommentId { get; set; }
        public int ComponentId { get; set; }
        public string CommentTime { get; set; }
        public int IssueCodeId { get; set; }
        public string EvaluatorComment { get; set; }
        public short CommentRating { get; set; }
    }

    [CollectionDataContract]
    public class FlightComments : List<FlightComment>
    {
        public FlightComments() { }
        public FlightComments(List<FlightComment> FlightComments) : base(FlightComments) { }
    }
    #endregion

    #region "EngineRuns"
    public class EngineRun
    {
        public int EngineRunId { get; set; }
        public int EngineSpeed { get; set; }
        public string RunStartTime { get; set; }
        public string RunEndTime { get; set; }
    }

    [CollectionDataContract]
    public class EngineRuns : List<EngineRun>
    {
        public EngineRuns() { }
        public EngineRuns(List<EngineRun> EngineRuns) : base(EngineRuns) { }
    }
    #endregion

    #region "RegionTimes"
    public class RegionTime
    {
        public int RegionTimeId { get; set; }
        public int RegionId { get; set; }
        public decimal Hours { get; set; }
    }

    [CollectionDataContract]
    public class RegionTimes : List<RegionTime>
    {
        public RegionTimes() { }
        public RegionTimes(List<RegionTime> RegionTimes) : base(RegionTimes) { }
    }
    #endregion

}
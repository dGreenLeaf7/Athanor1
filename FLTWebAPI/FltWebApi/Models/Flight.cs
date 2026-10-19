// Copyright (c) 2026 CODE Éxitos. All rights reserved.
// Athanor Workshop License 1.0 - see LICENSE at the repository root. Not open source.
using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Linq;
using System.Web;

namespace FltWebApi.Models
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

        public List<FlightComment> FlightComments { get; set; }
        public List<EngineRun> EngineRuns { get; set; }
        public List<RegionTime> RegionTimes { get; set; }
        public List<LiquidFill> LiquidFills { get; set; }

        public Flight()
        {
            this.FlightComments = new List<FlightComment>();
            this.LiquidFills = new List<LiquidFill>();
            this.EngineRuns = new List<EngineRun>();
            this.RegionTimes = new List<RegionTime>();
        }

    }

    [CollectionDataContract(Name="Flights", ItemName="Flight")]
    public class Flights : List<Flight>
    {
        public Flights() { }
        public Flights(List<Flight> Flights) : base(Flights) { }
    }
}
// Copyright (c) 2026 CODE Éxitos. All rights reserved.
// Athanor Workshop License 1.0 - see LICENSE at the repository root. Not open source.
using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace FltRestService.Model
{
    public class FlightInfo
    {
        public int FlightId { get; set; }
        public Guid FlightGuid { get; set; }
        public string FlightDate { get; set; }
        public decimal AirframeHoursStart { get; set; }
        public decimal AirframeHoursEnd { get; set; }
        public string DepartureTime { get; set; }
        public string ReturnTime { get; set; }
    }

    [CollectionDataContract]
    public class FlightInfos : List<FlightInfo>
    {
        public FlightInfos() { }
        public FlightInfos(List<FlightInfo> FlightInfos) : base(FlightInfos) { }
    }
}
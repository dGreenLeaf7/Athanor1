// Copyright (c) 2026 CODE Éxitos. All rights reserved.
// Athanor Workshop License 1.0 - see LICENSE at the repository root. Not open source.
using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace FltRestService.Model
{
    public class Aircraft
    {
        public int AircraftId { get; set; }
        public string ModelName { get; set; }
        public string MSN { get; set; }
        public string RegistrationNbr { get; set; }
        public string Description { get; set; }
        public int CurrentLocationId { get; set; }
        public int MeterUnitId { get; set; }
        public decimal AirframeHoursCurrent { get; set; }
        public decimal AirframeHoursOffset { get; set; }
    }

    [CollectionDataContract]
    public class Fleet : List<Aircraft>
    {
        public Fleet() { }
        public Fleet(List<Aircraft> Fleet) : base(Fleet) { }
    }
}
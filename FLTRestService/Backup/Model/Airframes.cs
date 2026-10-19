// Copyright (c) 2026 CODE Éxitos. All rights reserved.
// Athanor Workshop License 1.0 - see LICENSE at the repository root. Not open source.
using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace FltRestService.Model
{
    public class Airframe
    {
        public int AirframeId { get; set; }
        public string Description { get; set; }
        public string Weight { get; set; }
        public string WeightUnit { get; set; }
    }

    [CollectionDataContract]
    public class Airframes : List<Airframe>
    {
        public Airframes() { }
        public Airframes(List<Airframe> Airframes) : base(Airframes) { }
    }

}
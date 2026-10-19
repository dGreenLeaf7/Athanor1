// Copyright (c) 2026 CODE Éxitos. All rights reserved.
// Athanor Workshop License 1.0 - see LICENSE at the repository root. Not open source.
using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Linq;
using System.Web;

namespace FltWebApi.Models
{
    public class RegionTime
    {
        public int RegionTimeId { get; set; }
        public int RegionId { get; set; }
        public decimal Hours { get; set; }
    }

    [CollectionDataContract(Name="RegionTimes", ItemName="RegionTime")]
    public class RegionTimes :List<RegionTime>
    {
        public RegionTimes() { }
        public RegionTimes(List<RegionTime> RegionTimes) :base(RegionTimes) { }
    }
}
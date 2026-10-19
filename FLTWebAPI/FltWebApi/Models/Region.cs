// Copyright (c) 2026 CODE Éxitos. All rights reserved.
// Athanor Workshop License 1.0 - see LICENSE at the repository root. Not open source.
using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Linq;
using System.Web;

namespace FltWebApi.Models
{
    public class Region
    {
        public int Id { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }
    }

    [CollectionDataContract(Name="Regions", ItemName="Region")]
    public class Regions: List<Region>
    {
        public Regions() { }
        public Regions(List<Region> Regions) : base(Regions) { }
    }
}
// Copyright (c) 2026 CODE Éxitos. All rights reserved.
// Athanor Workshop License 1.0 - see LICENSE at the repository root. Not open source.
using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Linq;
using System.Web;

namespace FltWebApi.Models
{
    public class Location
    {
        public int LocationId { get; set; }
        public int ProgId { get; set; }
        public string LocationName { get; set; }
    }

    [CollectionDataContract(Name="Locations", ItemName="Location")]
    public class Locations: List<Location>
    {
        public Locations() { }
        public Locations(List<Location> Locations) : base(Locations) { }
    }
}
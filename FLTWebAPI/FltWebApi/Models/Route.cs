// Copyright (c) 2026 CODE Éxitos. All rights reserved.
// Athanor Workshop License 1.0 - see LICENSE at the repository root. Not open source.
using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Linq;
using System.Web;

namespace FltWebApi.Models
{
    public class Route
    {
        public int RouteId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
    }

    [CollectionDataContract(Name="Routes", ItemName="Route")]
    public class Routes: List<Route>
    {
        public Routes() { }
        public Routes(List<Route> Routes) : base(Routes) { }
    }
}
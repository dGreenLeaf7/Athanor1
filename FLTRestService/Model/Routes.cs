// Copyright (c) 2026 CODE Éxitos. All rights reserved.
// Athanor Workshop License 1.0 - see LICENSE at the repository root. Not open source.
using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace FltRestService.Model
{
    public class Route
    {
        public int RouteId { get; set; }
		public string Name { get; set; }
        public string Description { get; set; }
    }

    [CollectionDataContract]
    public class Routes : List<Route>
    {
        public Routes() { }
        public Routes(List<Route> Routes) : base(Routes) { }
    }
}
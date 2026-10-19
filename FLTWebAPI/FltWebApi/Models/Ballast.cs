// Copyright (c) 2026 CODE Éxitos. All rights reserved.
// Athanor Workshop License 1.0 - see LICENSE at the repository root. Not open source.
using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Linq;
using System.Web;

namespace FltWebApi.Models
{
    public class Ballast
    {
        public int BallastId { get; set; }
        public string Description { get; set; }
        public string Weight { get; set; }
        public string WeightUnit { get; set; }
    }

    [CollectionDataContract(Name="Dollies", ItemName="Ballast")]
    public class Dollies: List<Ballast>
    {
        public Dollies() { }
        public Dollies(List<Ballast> Dollies) : base(Dollies) { }
    }
}
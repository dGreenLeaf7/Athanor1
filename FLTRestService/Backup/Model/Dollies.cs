// Copyright (c) 2026 CODE Éxitos. All rights reserved.
// Athanor Workshop License 1.0 - see LICENSE at the repository root. Not open source.
using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace FltRestService.Model
{
    public class Ballast
    {
        public int BallastId { get; set; }
        public string Description { get; set; }
        public string Weight { get; set; }
        public string WeightUnit { get; set; }
    }

    [CollectionDataContract]
    public class Dollies : List<Ballast>
    {
        public Dollies() { }
        public Dollies(List<Ballast> Dollies) : base(Dollies) { }
    }

}
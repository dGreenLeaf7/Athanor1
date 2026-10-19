// Copyright (c) 2026 CODE Éxitos. All rights reserved.
// Athanor Workshop License 1.0 - see LICENSE at the repository root. Not open source.
using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace FltRestService.Model
{
    public class Program
    {
        public int ProgID { get; set; }
        public string ProgName { get; set; }
        public string ProgDescription { get; set; }
        public string ProgVersion { get; set; }
    }

    [CollectionDataContract]
    public class Programs : List<Program>
    {
        public Programs() { }
        public Programs(List<Program> Programs) : base(Programs) { }
    }
}
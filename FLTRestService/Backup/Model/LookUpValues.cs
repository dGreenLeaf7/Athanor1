// Copyright (c) 2026 CODE Éxitos. All rights reserved.
// Athanor Workshop License 1.0 - see LICENSE at the repository root. Not open source.
using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace FltRestService.Model
{
    public class LookUpValue
    {
        public int ID { get; set; }
        public int SortOrder { get; set; }
        public string StringValue { get; set; }
    }

    [CollectionDataContract]
    public class LookUpValues : List<LookUpValue>
    {
        public LookUpValues() { }
        public LookUpValues(List<LookUpValue> LookUpValues) : base(LookUpValues) { }
    }
}
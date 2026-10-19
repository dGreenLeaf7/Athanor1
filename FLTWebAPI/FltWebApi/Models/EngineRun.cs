// Copyright (c) 2026 CODE Éxitos. All rights reserved.
// Athanor Workshop License 1.0 - see LICENSE at the repository root. Not open source.
using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Linq;
using System.Web;

namespace FltWebApi.Models
{
    public class EngineRun
    {
        public int EngineRunId { get; set; }
        public int EngineSpeed { get; set; }
        public string RunStartTime { get; set; }
        public string RunEndTime { get; set; }
    }

    [CollectionDataContract(Name = "EngineRuns", ItemName="EngineRun")]
    public class EngineRuns : List<EngineRun>
    {
        public EngineRuns() { }
        public EngineRuns(List<EngineRun> EngineRuns) : base(EngineRuns) { }
    }
}
// Copyright (c) 2026 CODE Éxitos. All rights reserved.
// Athanor Workshop License 1.0 - see LICENSE at the repository root. Not open source.
using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Linq;
using System.Web;

namespace FltWebApi.Models
{
    public class LiquidFill
    {
        public int LiquidFillId { get; set; }
        public int LiquidFillTypeId { get; set; }
        public int LiquidFillSubTypeId { get; set; }
        public int LiquidUnitId { get; set; }
        public decimal LiquidAmt { get; set; }
        public decimal HobbsReading { get; set; }
        public string LiquidFillLocation { get; set; }
        public string LiquidFillApprover { get; set; }
    }

    [CollectionDataContract(Name="LiquidFills", ItemName="LiquidFill")]
    public class LiquidFills: List<LiquidFill>
    {
        public LiquidFills() { }
        public LiquidFills(List<LiquidFill> LiquidFills) : base(LiquidFills) { }
    }
}
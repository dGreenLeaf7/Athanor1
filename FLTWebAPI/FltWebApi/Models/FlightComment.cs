// Copyright (c) 2026 CODE Éxitos. All rights reserved.
// Athanor Workshop License 1.0 - see LICENSE at the repository root. Not open source.
using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Linq;
using System.Web;

namespace FltWebApi.Models
{
    public class FlightComment
    {
        public int FlightCommentId { get; set; }
        public int ComponentId { get; set; }
        public string CommentTime { get; set; }
        public int IssueCodeId { get; set; }
        public string EvaluatorComment { get; set; }
        public short CommentRating { get; set; }
    }

    [CollectionDataContract(Name = "FlightComments", ItemName = "FlightComment")]
    public class FlightComments : List<FlightComment>
    {
        public FlightComments() { }
        public FlightComments(List<FlightComment> FlightComments) : base(FlightComments) { }
    }

}
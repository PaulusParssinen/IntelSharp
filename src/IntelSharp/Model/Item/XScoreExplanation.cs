using System;
using System.Collections.Generic;

namespace IntelSharp.Model
{
    public class XScoreExplanation
    {
        public Guid SystemId { get; set; }
        public int XScore { get; set; }
        public IEnumerable<ScoreReason> Breakdown { get; set; }
    }
}

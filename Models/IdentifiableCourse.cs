using System;
using System.Collections.Generic;

namespace Schedule4Me.Models
{
    public class IdentifiableCourse
    {
        public string Prefix { get; set; }
        public string Number { get; set; }
        public string SectionNumber { get; set; }
        public int Code { get; set; }
        public int Value { get; set; }
        // List of Day,Time
        public List<Tuple<int, int>> TimeIntervals { get; set; }

        public IdentifiableCourse()
        {
            TimeIntervals = new List<Tuple<int, int>>();
        }
    }
}
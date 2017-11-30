using System;
using System.Collections.Generic;

namespace Schedule4Me.Models
{
    public class IdentifiableSection
    {
        public string Prefix { get; set; }
        public string Number { get; set; }
        public string SectionNumber { get; set; }
        public int Code { get; set; }
        public int Value { get; set; }
        // List of Day,Time
        public List<Tuple<int, int>> TimeIntervals { get; set; }

        public IdentifiableSection()
        {
            TimeIntervals = new List<Tuple<int, int>>();
        }

        public override string ToString() {
            return Prefix + " " + Number + " Section " + SectionNumber;
        }

        public override bool Equals(object obj) {
            var section = obj as IdentifiableSection;
            return section != null && section.Number == Number && section.Code == Code;
        }
    }
}
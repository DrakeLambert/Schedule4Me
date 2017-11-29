using System.Collections;
using System.Collections.Generic;

namespace Schedule4Me.Models
{
    public class Schedule : IEnumerable
    {
        // Dictionary of days 
        // Dictionary of times
        // List of courses
        private Dictionary<int, Dictionary<int, List<IdentifiableCourse>>> _schedule;

        public Schedule()
        {
            _schedule = new Dictionary<int, Dictionary<int, List<IdentifiableCourse>>>();
            for (var i = 0; i < 7; i++)
            {
                _schedule.Add(i, new Dictionary<int, List<IdentifiableCourse>>());
            }
        }

        public void Add(IdentifiableCourse course)
        {
            foreach (var interval in course.TimeIntervals)
            {
                var day = _schedule[interval.Item1];
                if (day.ContainsKey(interval.Item2))
                {
                    day[interval.Item2].Add(course);
                }
                else
                {
                    day.Add(interval.Item2, new List<IdentifiableCourse> { course });
                }
            }
        }

        public void AddRange(IEnumerable<IdentifiableCourse> courses)
        {
            foreach (var course in courses)
            {
                Add(course);
            }
        }

        public IEnumerator GetEnumerator()
        {
            return _schedule.GetEnumerator();
        }
    }
}
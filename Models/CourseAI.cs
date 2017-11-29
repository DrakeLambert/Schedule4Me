using System;
using System.Collections;
using System.Collections.Generic;

namespace Schedule4Me.Models
{
    public static class CourseAI
    {
        public static List<Course> Schedule(this List<Course> courses)
        {
            return courses;
        }
    }

    public class IdentifiableCourse
    {
        public string Prefix { get; set; }
        public string Number { get; set; }
        public string SectionNumber { get; set; }
        public int Code { get; set; }
        // List of Day,Time
        public List<Tuple<int, int>> TimeIntervals { get; set; }
    }

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
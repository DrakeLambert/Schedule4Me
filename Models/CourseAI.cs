using System;
using System.Collections;
using System.Collections.Generic;

namespace Schedule4Me.Models
{
    public static class CourseAI
    {
        public static List<Course> Schedule(this List<Course> courses)
        {
            var schedule = new Schedule();

            foreach (var course in courses)
            {
                foreach (var section in course.sections)
                {
                    var iCourse = new IdentifiableCourse
                    {
                        Code = course.GetHashCode(),
                        Number = course.number,
                        Prefix = course.abbreviation,
                        SectionNumber = section.number
                    };
                    foreach (var interval in section.timeIntervals)
                    {
                        // interval.
                        // iCourse.TimeIntervals.Add(())
                    }
                }
            }
            return courses;
        }

        // private List<int> StringIntervalToInt(string start, string end)
        // {

        // }

        // private int StringTimeToInt(string time)
        // {
        //     var hour = time.Substring(0, 2)
        //         .Apply(Convert.ToInt32);
        //     var minute = time.Substring(2, 2)
        //         .Apply(Convert.ToInt32);

        //     if (time.EndsWith('N'))
        //     {
        //         hour
        //     }
        // }

        private static F Apply<T, F>(this T value, Func<T, F> function)
        {
            return function(value);
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

        public IdentifiableCourse()
        {
            TimeIntervals = new List<Tuple<int, int>>();
        }
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
using System;
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
                        SectionNumber = section.number,
                        Value = courses.IndexOf(course)
                    };
                    foreach (var interval in section.timeIntervals)
                    {
                        var times = StringIntervalToInt(interval.start, interval.end);

                        foreach (var day in interval.days)
                        {
                            int dayInt = DayStringToInt(day);
                            foreach (var time in times)
                            {
                                iCourse.TimeIntervals.Add(new Tuple<int, int>(dayInt, time));
                            }
                        }
                    }
                    schedule.Add(iCourse);
                }
            }






            return courses;
        }

        private static int DayStringToInt(string day)
        {
            switch (day.ToLower())
            {
                case "sunday":
                    return 0;
                case "monday":
                    return 1;
                case "tuesday":
                    return 2;
                case "wednesday":
                    return 3;
                case "thursday":
                    return 4;
                case "friday":
                    return 5;
                case "saturday":
                    return 6;
            }
            return 0;
        }

        private static List<int> StringIntervalToInt(string start, string end)
        {
            var startInt = StringTimeToInt(start);
            var endInt = StringTimeToInt(end);
            var times = new List<int>();
            for (; startInt < endInt; startInt++)
            {
                times.Add(startInt);
            }
            return times;
        }

        private static int StringTimeToInt(string time)
        {
            if (time.Length < 5)
            {
                if (time.EndsWith('N') || time.Length < 4)
                {
                    time = '0' + time;
                }
            }

            var hour = time.Substring(0, 2)
                .Apply(Convert.ToInt32);
            var minute = time.Substring(2, 2)
                .Apply(Convert.ToInt32);

            hour += 1;
            if (hour < 6 || time.EndsWith('N'))
            {
                // Afternoon
                hour += 12;
            }
            hour *= 2;
            if (minute <= 30)
            {
                return hour;
            }
            return hour + 1;
        }

        private static F Apply<T, F>(this T value, Func<T, F> function)
        {
            return function(value);
        }
    }
}
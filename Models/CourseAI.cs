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
                        SectionNumber = section.number
                    };
                    foreach (var interval in section.timeIntervals)
                    {
                        var times = StringIntervalToInt(interval.start, interval.end);

                        foreach (var day in interval.days)
                        {
                            int dayInt;
                            switch (day.ToLower())
                            {
                                case "sunday":
                                    dayInt = 0;
                                    break;
                                case "monday":
                                    dayInt = 1;
                                    break;
                                case "tuesday":
                                    dayInt = 2;
                                    break;
                                case "wednesday":
                                    dayInt = 3;
                                    break;
                                case "thursday":
                                    dayInt = 4;
                                    break;
                                case "friday":
                                    dayInt = 5;
                                    break;
                                case "saturday":
                                    dayInt = 6;
                                    break;
                                default:
                                    dayInt = 0;
                                    break;
                            }
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
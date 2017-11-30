using System;
using System.Collections.Generic;
using System.Linq;

namespace Schedule4Me.Models
{
    public static class CourseAI
    {
        public static List<IdentifiableSection> Schedule(this List<Course> courses)
        {
            if (courses.Count < 1)
            {
                return new List<IdentifiableSection>();
            }

            var iCourses = new List<List<IdentifiableSection>>();

            foreach (var course in courses)
            {
                var iCourse = new List<IdentifiableSection>();
                iCourses.Add(iCourse);
                foreach (var section in course.sections)
                {
                    var iSection = new IdentifiableSection
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
                                iSection.TimeIntervals.Add(new Tuple<int, int>(dayInt, time));
                            }
                        }
                    }
                    iCourse.Add(iSection);
                }
            }

            var outSchedule = new List<IdentifiableSection>();

            var i = 1;
            var deepest = 0;
            var j = 0;
            outSchedule.Add(iCourses[0][0]);
            while (outSchedule.Count < iCourses.Count)
            {
                if (iCourses[i].Count > j)
                {
                    for (var k = 0; k < i; k++)
                    {
                        var conflict = false;
                        foreach (var sect in outSchedule)
                        {
                            if (iCourses[i][j].HasConflict(sect))
                            {
                                conflict = true;
                                break;
                            }
                        }
                        if (conflict)
                        {
                            k = -1;
                            if (iCourses[i].Count > j + 1)
                            {
                                j++;
                            }
                            else
                            {
                                i--;
                                j = iCourses.Where(course => course.Contains(outSchedule[i])).First().IndexOf(outSchedule[i]) + 1;
                                if (i == 0 && iCourses[i].Count <= j)
                                {
                                    iCourses.Remove(iCourses[deepest]);
                                    i = deepest;
                                    j = 0;
                                    break;
                                }
                            }
                        }
                        else
                        {
                            outSchedule.Add(iCourses[i][j]);
                            j = 0;
                            i++;
                            break;
                        }
                    }
                }
                if (i > deepest)
                {
                    deepest = i;
                }
            }

            return outSchedule;
        }

        private static bool HasConflict(this IdentifiableSection section1, IdentifiableSection section2)
        {
            foreach (var interval1 in section1.TimeIntervals)
            {
                foreach (var interval2 in section2.TimeIntervals)
                {
                    if (interval1.Item1 == interval2.Item1 && interval1.Item2 == interval2.Item2)
                    {
                        return true;
                    }
                }
            }
            return false;
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
            for (; startInt <= endInt; startInt++)
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

            var hour = Convert.ToInt32(time.Substring(0, 2));
            var minute = Convert.ToInt32(time.Substring(2, 2));

            //hour += 1;
            if (hour < 6 || time.EndsWith('N'))
            {
                // Afternoon
                hour += 12;
            }
            hour *= 2;
            if (minute < 30)
            {
                return hour;
            }
            return hour + 1;
        }
    }
}
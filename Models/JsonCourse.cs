using System;
using System.Linq;

namespace Schedule4Me.Models
{
    public class Course
    {
        public string abbreviation { get; set; }
        public string number { get; set; }
        public string hours { get; set; }
        public string full_title { get; set; }
        public string description { get; set; }
        public string[] comments { get; set; }
        public Section[] sections { get; set; }

        public override string ToString()
        {
            return $"{abbreviation} {number}";
        }

        public override bool Equals(object obj)
        {
            var course = obj as Course;
            return course != null && course.abbreviation.ToLower() == abbreviation.ToLower() && course.number == number;
        }

        public override int GetHashCode()
        {
            var array = abbreviation.Select(ch => (int)ch).ToArray();
			var code = 0;
            for (int i = 0; i < array.Length; i++)
            {
                code += array[i] * Convert.ToInt32(Math.Pow(10, array.Length - i - 1));
            }
			return code;
        }
    }

    public class Section
    {
        public string number { get; set; }
        public string title { get; set; }
        public string enrollment_available { get; set; }
        public string enrollment_current { get; set; }
        public string enrollment_is_full { get; set; }
        public string enrollment_total { get; set; }
        public Timeinterval[] timeIntervals { get; set; }
    }

    public class Timeinterval
    {
        public string start { get; set; }
        public string end { get; set; }
        public bool has_time { get; set; }
        public string[] days { get; set; }
        public object[] comments { get; set; }
        public string location_building { get; set; }
        public string location_room { get; set; }
        public bool is_lab { get; set; }
        public bool s_night { get; set; }
        public bool s_all_web { get; set; }
        public bool s_most_web { get; set; }
        public bool s_half_web { get; set; }
        public bool s_some_web { get; set; }
        public bool s_req_dept_perm { get; set; }
        public bool s_req_inst_perm { get; set; }
        public bool s_majors_only { get; set; }
        public bool s_cmi { get; set; }
        public bool s_cmi_written { get; set; }
        public bool s_cmi_spoken { get; set; }
        public bool s_cmi_tech { get; set; }
        public bool s_cmi_visual { get; set; }
        public bool s_svc { get; set; }
        public Instructor[] instructor { get; set; }
    }

    public class Instructor
    {
        public string name { get; set; }
    }

}
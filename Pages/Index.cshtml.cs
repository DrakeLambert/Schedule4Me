using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using Schedule4Me.Models;
using System.Text.RegularExpressions;
using System.Net.Http;
using System;
using Newtonsoft.Json;

namespace Schedule4Me.Pages
{
    public class IndexModel : PageModel
    {
        [BindProperty]
        public string Courses { get; set; }

        public List<Course> FormattedCourses { get; set; }

        private ILogger<IndexModel> _logger;

        private const string _coursePrefixPattern = "[A-z]{3,4}";

        private const string _courseNumberPattern = "[0-9]{4}";

        private const string _courseNamePattern = _coursePrefixPattern + " *" + _courseNumberPattern;

        private const string _apiEndpoint = "https://lsu-api.herokuapp.com";

        private CourseCache _courseCache;

        public IndexModel(ILogger<IndexModel> logger, CourseCache courseCache)
        {
            _logger = logger;
            _courseCache = courseCache;
            FormattedCourses = new List<Course>();
        }

        public IActionResult OnPost()
        {
            if (Courses == null)
            {
                return Page();
            }

            var knownDepartments = new Dictionary<string, List<Course>>();

            FormattedCourses =
                Courses
                .Split(',')
                .AsQueryable()
                .Select(userInput => Regex.Match(userInput, _courseNamePattern))
                .Where(match => match.Success == true)
                .Select(match => match.Value.ToLower())
                .Select(courseName => _courseCache.GetCourse(GetPrefix(courseName), GetNumber(courseName)))
                .ToList();

            return Page();
        }

        private List<Course> GetDepartment(string department)
        {
            return JsonConvert.DeserializeObject<List<Course>>
            (
                new HttpClient
                {
                    BaseAddress = new Uri(_apiEndpoint)
                }
                .GetStringAsync($"department?dept={department}")
                .Result
            );
        }

        private string GetPrefix(string courseName)
        {
            return Regex.Matches(courseName, _coursePrefixPattern)
                .Where(m => m.Success)
                .Aggregate((m1, m2) =>
                {
                    if (m1.Length > m2.Length)
                    {
                        return m1;
                    }
                    else
                    {
                        return m2;
                    }
                })
                .Value;
        }

        private string GetNumber(string courseName)
        {
            return Regex.Matches(courseName, _courseNumberPattern)
                .Where(m => m.Success)
                .First()
                .Value;
        }
    }
}
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace Schedule4Me.Models
{
    public class CourseCache
    {
        private ConcurrentDictionary<string, IEnumerable<Course>> _knownDepartments;

        private const string _apiEndpoint = "https://lsu-api.herokuapp.com";

        private HttpClient _client;

        private Task _updateTask;

        public CourseCache()
        {
            _client = new HttpClient
            {
                BaseAddress = new Uri(_apiEndpoint)
            };
            _knownDepartments = new ConcurrentDictionary<string, IEnumerable<Course>>();

            _updateTask = Task.Run(() =>
            {
                Task.Delay(TimeSpan.FromDays(1));
                _knownDepartments.Clear();
            });
        }

        public Course GetCourse(string prefix, string number)
        {
            return _knownDepartments.GetOrAdd(prefix,
                (p) =>
                    JsonConvert.DeserializeObject<IEnumerable<Course>>(
                        _client.GetStringAsync($"department?dept={prefix}").Result
                ))
                .Where(course => course.number == number).FirstOrDefault();
        }
    }
}

using System;
using System.Collections.Generic;
using System.Net.Http;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Newtonsoft.Json;
using Schedule4Me.Models;

namespace Schedule4Me
{
	public class Program
    {
        public static void Main(string[] args)
        {
            var client = new HttpClient();
            client.BaseAddress = new Uri("https://lsu-api.herokuapp.com");
            var deptAbbr = Console.ReadLine();
            var returned = client.GetStringAsync($"department?dept={ deptAbbr }").Result;
            var courses = JsonConvert.DeserializeObject<List<Course>>(returned);

            Console.WriteLine($"Courses: { courses.Count }");
            //Console.WriteLine(returned);

            //BuildWebHost(args).Run();
        }

        public static IWebHost BuildWebHost(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                .UseStartup<Startup>()
                .Build();
    }
}

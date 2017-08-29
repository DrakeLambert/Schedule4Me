using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;

namespace Test
{
	class Program
	{
		static void Main(string[] args)
		{
			var client = new HttpClient
			{
				BaseAddress = new Uri("http://appl101.lsu.edu")
			};
			var responseBody = client.GetStringAsync("booklet2.nsf/All/45989982C33E5A0E862580E3002C3445?OpenDocument").Result;
			responseBody = responseBody.Substring(responseBody.IndexOf("<PRE>") + 5);
			responseBody = responseBody.Remove(responseBody.IndexOf("</PRE>"));

			var CSC = new College
			{
				Abbreviation = "CSC",
				Courses = new List<Course>()
			};
			foreach (var line in responseBody.Split(Environment.NewLine).Skip(3))
			{
				if (string.IsNullOrWhiteSpace(line))
				{
					continue;
				}
				try
				{
					var section = new Section
					{
						Number = line.Substring(28, 3),
						StartMinute = int.Parse(line.Substring(30, 4)),
						EndMinute = int.Parse
					};
				}
				catch (Exception e)
				{
					Console.WriteLine($"Error on line \"{ line }\" - { e.Message }");
				}
			}

			Console.WriteLine(responseBody);
			Console.ReadLine();
		}
	}

	public class Section
	{
		public string Number { get; set; }
		public int StartMinute { get; set; }
		public int EndMinute { get; set; }
	}

	public class Course
	{
		public string Number { get; set; }
		public string Title { get; set; }
		public List<Section> Sections { get; set; }
	}

	public class College
	{
		public string Abbreviation { get; set; }
		public List<Course> Courses { get; set; }
	}
}

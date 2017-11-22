using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Newtonsoft.Json;
using Schedule4Me.Models;

namespace Schedule4Me.Pages
{
	public class IndexModel : PageModel
	{
		public List<Course> Courses { get; private set; }

		public void OnGet()
		{
			var client = new HttpClient();
			client.BaseAddress = new Uri("https://lsu-api.herokuapp.com");

			Courses = JsonConvert.DeserializeObject<List<Course>>(client.GetStringAsync("department?dept=csc").Result);
		}
	}
}
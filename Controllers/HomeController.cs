using Microsoft.AspNetCore.Mvc;

namespace Schedule4Me.Controllers
{
	/// <summary>
	/// Serves methods for controlling the home page
	/// </summary>
	[Route("")]
	public class HomeController : Controller
	{
		/// <summary>
		/// Gets the home page
		/// </summary>
		/// <returns>The home page view</returns>
		[HttpGet("")]
		public IActionResult Index() {
			return View();
		}
	}
}
using Microsoft.AspNetCore.Mvc;

namespace MyLibrary.Web.Controllers
{
	public class LoanController : Controller
	{
		public IActionResult Index()
		{
			return View();
		}
	}
}

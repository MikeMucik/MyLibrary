using Microsoft.AspNetCore.Mvc;

namespace MyLibraryHome.Controllers
{
	public class LoanController : Controller
	{
		public IActionResult Index()
		{
			return View();
		}
	}
}

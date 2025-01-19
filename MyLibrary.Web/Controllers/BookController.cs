using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MyLibraryMVC.Application.Interfaces;
using MyLibraryMVC.Application.ViewModels.Book;


namespace MyLibrary.Web.Controllers
{
	public class BookController : Controller
	{
		private readonly IBookService _bookService;
		public BookController(IBookService bookService)
		{
			_bookService = bookService;
		}
		// GET: BookController
		public IActionResult Index(int pageSize =12, int pageNumber =1, string searchString="")
		{
			var model = _bookService.GetAllBooks(pageSize, pageNumber, searchString);
			return View(model);
		}

		// GET: BookController/Details/5
		public IActionResult Details(int id)
		{
			var model = _bookService.GetBookDetails(id);
			return View(model);
		
		}

		// GET: BookController/Create
		public ActionResult Create()
		{
			return View();
		}

		// POST: BookController/Create
		[HttpPost]
		[ValidateAntiForgeryToken]
		public ActionResult Create(IFormCollection collection)
		{
			try
			{
				return RedirectToAction(nameof(Index));
			}
			catch
			{
				return View();
			}
		}

		// GET: BookController/Edit/5
		public ActionResult Edit(int id)
		{
			return View();
		}

		// POST: BookController/Edit/5
		[HttpPost]
		[ValidateAntiForgeryToken]
		public ActionResult Edit(int id, IFormCollection collection)
		{
			try
			{
				return RedirectToAction(nameof(Index));
			}
			catch
			{
				return View();
			}
		}

		// GET: BookController/Delete/5
		public ActionResult Delete(int id)
		{
			return View();
		}

		// POST: BookController/Delete/5
		[HttpPost]
		[ValidateAntiForgeryToken]
		public ActionResult Delete(int id, IFormCollection collection)
		{
			try
			{
				return RedirectToAction(nameof(Index));
			}
			catch
			{
				return View();
			}
		}
	}
}

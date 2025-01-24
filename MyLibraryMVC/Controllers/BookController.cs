using Microsoft.AspNetCore.Mvc;
using MyLibraryMVC.Application.Interfaces;
using MyLibraryMVC.Application.ViewModels.Book;

namespace MyLibraryMVC.Controllers
{
	public class BookController : Controller
	{
		private readonly IBookService _bookService;
		private readonly ICategoryService _categoryService;
		public BookController(IBookService bookService,
								ICategoryService categoryService)
		{
			_bookService = bookService;
			_categoryService = categoryService;
		}
		[HttpGet]
		public IActionResult IndexBook(int PageSize = 12, int PageNumber = 1, string SearchString = "")
		{
			var model = _bookService.GetAllBooks(PageSize, PageNumber, SearchString);
			return View(model);
		}
		[HttpGet]
		public IActionResult BookDetails(int id)
		{
			var model = _bookService.GetBookDetails(id);
			return View(model);
		}
		[HttpGet]
		public IActionResult AddBook()
		{
			FillViewBags();
			return View(new NewBookVm());
		}
		[HttpPost]
		public IActionResult AddBook(NewBookVm model)
		{
			FillViewBags();
			int recipeId = 0;
			if (ModelState.IsValid)
			{
				recipeId = _bookService.AddBook(model);
			}
			return View(BookDetails(recipeId));
		}
		public void FillViewBags()
		{
			ViewBag.Categories = _categoryService.GetCategoryForSelectList();
		}
	}
}

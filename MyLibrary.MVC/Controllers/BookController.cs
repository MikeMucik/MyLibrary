using AspNetCoreGeneratedDocument;
using Microsoft.AspNetCore.Mvc;
using MyLibraryMVC.Application.Interfaces;
using MyLibraryMVC.Application.ViewModels.Book;
using MyLibraryMVC.Application.ViewModels.BookInfo;

namespace MyLibrary.MVC.Controllers
{
	public class BookController : Controller
	{
		private readonly IBookService _bookService;
		private readonly ICategoryService _categoryService;
		private readonly IAgeGroupService _ageGroupService;	
		private readonly IBookInfoService _bookInfoService;
		public BookController(
			IBookService bookService,
			ICategoryService categoryService,
			IAgeGroupService ageGroupService,
			IBookInfoService bookInfoService)
		{
			_bookService = bookService;
			_categoryService = categoryService;
			_ageGroupService = ageGroupService;
			_bookInfoService = bookInfoService;
		}
		[HttpGet]
		public IActionResult BookIndex(int pageSize= 12, int pageNumber= 1, string searchString = "")
		{
			var model = _bookService.GetAllBooks(pageSize , pageNumber , searchString );
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
			if (ModelState.IsValid)
			{
				int bookId = _bookService.AddBook(model);
				return RedirectToAction("BookDetails", new { id = bookId });
			}
			return View(model);
		}
		//[HttpGet]
		//public PartialViewResult AddBookInfo()
		//{
		//	FillViewBags();
		//	return PartialView("_AddBookInfo");
		//}
		//[HttpPost]
		//public IActionResult AddBookInfo(NewBookInfoVm model)
		//{
		//	FillViewBags();
		//	var bookId = 0;
		//	if (ModelState.IsValid)
		//	{
		//		_bookInfoService.AddInfoBook(model);
		//	}
		//	return RedirectToAction("BookDetails", new { id = bookId });
		//}
		public void FillViewBags()
		{
			ViewBag.Categories = _categoryService.GetCategoryForSelectList();
			ViewBag.AgeGroups = _ageGroupService.GetAgeGroupForSelecetList();
		}
	}
}

using Microsoft.AspNetCore.Mvc;
using MyLibraryMVC.Application.Interfaces;
using MyLibraryMVC.Application.ViewModels.Author;
using MyLibraryMVC.Application.ViewModels.Book;
using MyLibraryMVC.Application.ViewModels.BookInfo;

namespace MyLibraryHome.Controllers
{
	public class BookController : Controller
	{
		private readonly IBookService _bookService;
		private readonly ICategoryService _categoryService;
		private readonly IAgeGroupService _ageGroupService;
		private readonly IBookInfoService _bookInfoService;
		private readonly IHouseService _houseService;
		private readonly ICityService _cityService;
		private readonly IAuthorService _authorService;
		public BookController(
			IBookService bookService,
			ICategoryService categoryService,
			IAgeGroupService ageGroupService,
			IBookInfoService bookInfoService,
			IHouseService houseService,
			ICityService cityService,
			IAuthorService authorService)
		{
			_bookService = bookService;
			_categoryService = categoryService;
			_ageGroupService = ageGroupService;
			_bookInfoService = bookInfoService;	
			_houseService = houseService;
			_cityService = cityService;
			_authorService = authorService;
		}
		[HttpGet]
		public IActionResult BookIndex(int pageSize = 12, int pageNumber = 1, string searchString = "")
		{
			var model = _bookService.GetAllBooks(pageSize, pageNumber, searchString);
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
		[HttpGet]
		public IActionResult AddBookInfo()
		{
			FillViewBags();			
			return PartialView("_AddBookInfo");				
		}
		[HttpGet]
		public IActionResult AddPublishingInfo()
		{
			FillViewBags();
			return PartialView("_AddInfo");
		}
		[HttpGet]
		public IActionResult FindBook()
		{
			FillViewBags();
			return View(new FindBookVm());
		}
		
		public void FillViewBags()
		{
			ViewBag.Categories = _categoryService.GetCategoryForSelectList();
			ViewBag.AgeGroups = _ageGroupService.GetAgeGroupForSelecetList();
			ViewBag.Houses = _houseService.GetHousesForSelectList();
			ViewBag.Cities = _cityService.GetCitiesForSelectList();
			ViewBag.Authors = _authorService.GetAuthorsForSelectList();
		}
	}
}
//var model = new NewBookVm()
//{
//	Authors = new List<NewAuthorVm>
//			};
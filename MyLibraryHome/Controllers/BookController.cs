using Microsoft.AspNetCore.Mvc;
using MyLibraryMVC.Application.Interfaces;
using MyLibraryMVC.Application.ViewModels.Author;
using MyLibraryMVC.Application.ViewModels.Book;
using MyLibraryMVC.Application.ViewModels.BookInfo;
using Newtonsoft.Json;

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
		public IActionResult FindBook(int pageSize = 12, int pageNumber = 1,
			int authorId= 0 ,int categoryId = 0, int ageGroupId = 0, int houseOfPublishingId = 0)
		{
			FillViewBags();
			var model = _bookService.FindBook(pageSize, pageNumber,
				authorId, categoryId, ageGroupId, houseOfPublishingId);
			return View(model);
		}
		[HttpPost]
		public IActionResult FindBook(int pageSize, int? pageNumber, int authorId,
			int categoryId, int ageGroupId,  int houseOfPublishingId)
			
		{
			if ( pageNumber == null || pageNumber ==0)
			{
				pageNumber = 1;
			} ;
				var model = _bookService.FindBook(pageSize, pageNumber.Value, 
				authorId ,categoryId, ageGroupId, houseOfPublishingId);
			FillViewBags();
			return View(model);
		}
		[HttpGet]
		public IActionResult BookEdit(int id)
		{
			FillViewBags();
			var model = _bookService.GetBookToEdit(id);
			Console.WriteLine(JsonConvert.SerializeObject(model.BookInfo, Formatting.Indented));

			return View(model);
		}
		[HttpPost]
		public IActionResult BookEdit(NewBookVm model)
		{
			FillViewBags();
			if (ModelState.IsValid)
			{
				var updatedBookId = _bookService.UpdateBook(model);
				return RedirectToAction("BookDetails", new { id = updatedBookId });
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

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MyLibraryMVC.Application.Interfaces;
using MyLibraryMVC.Application.ViewModels.Author;
using MyLibraryMVC.Application.ViewModels.Category;
using MyLibraryMVC.Application.ViewModels.ManagerUser;
using MyLibraryMVC.Infrastructure;

namespace MyLibraryHome.Controllers
{
	[Authorize]
	[AutoValidateAntiforgeryToken]
	public class OtherFunctionController : Controller
	{
		private readonly ICategoryService _categoryService;
		private readonly IAuthorService _authorService;
		private readonly UserManager<ApplicationUser> _userManager;
		public OtherFunctionController(
			ICategoryService categoryService,
			IAuthorService authorService,
			UserManager<ApplicationUser> userManager)
		{
			_categoryService = categoryService;
			_authorService = authorService;
			_userManager = userManager;
		}
		[HttpGet]
		public async Task<IActionResult> ManageUsers()
		{
			var users = await _userManager.Users.ToListAsync();
			var model = new ListManageUsersVm()
			{
				ListUsers = new List<ManageUserVm>()
			};
			foreach (var user in users)
			{
				var roles = await _userManager.GetRolesAsync(user);
				if (roles.Contains("Admin"))
				{
					Console.WriteLine("Jest Adminem");
				}
				model.ListUsers.Add(new ManageUserVm
				{
					UserId = user.Id,
					UserName = user.UserName,
					IsUser = roles.Contains("User"),					
					IsAdmin = roles.Contains("Admin")
				});
			}
			return View(model);
		}
		[HttpPost]
		public async Task<IActionResult> ManageUsers(ListManageUsersVm model)
		{
			foreach (var userVm in model.ListUsers)
			{
				var user = await _userManager.FindByIdAsync(userVm.UserId);
				if (user != null)
				{
					var currentRoles = await _userManager.GetRolesAsync(user);
					await _userManager.RemoveFromRolesAsync(user, currentRoles);
					if (userVm.IsAdmin)
					{
						await _userManager.AddToRoleAsync(user, "Admin");
					}					
					else if (userVm.IsUser)
					{
						await _userManager.AddToRoleAsync(user, "User");
					}
				}
			}
			return RedirectToAction(nameof(ManageUsers));
		}
		public IActionResult Index()
		{
			return View();
		}
		[HttpGet]		
		public IActionResult AuthorsInfo(int pageSize = 12, int pageNumber= 1)
		{
			var model = _authorService.GetAllAuthors(pageSize, pageNumber );
			return View(model);
		}
		[HttpGet]		
		public IActionResult AddAuthor()
		{
			return View(new NewAuthorVm());
		}
		[HttpPost]		
		public IActionResult AddAuthor(NewAuthorVm authorVm)
		{
			_authorService.GetOrAddAuthor(authorVm);
			return RedirectToAction("AuthorsInfo");
		}
		[HttpGet]		
		public IActionResult EditAuthor(int id)
		{
			var authorVm = _authorService.GetAuthorById(id);
			return View(authorVm);
		}
		[HttpPost]		
		public IActionResult EditAuthor(NewAuthorVm authorVm)
		{
			_authorService.EditAuthor(authorVm);
			return RedirectToAction("AuthorsInfo");
		}
		[HttpGet]		
		public IActionResult DeleteAuthor(int id)
		{
			_authorService.DeleteAuthor(id);
			return RedirectToAction("AuthorsInfo");
		}
		[HttpGet]		
		public IActionResult AddCategory()
		{
			return View(new NewCategoryVm());
		}
		[HttpPost]		
		public IActionResult AddCategory(NewCategoryVm category)
		{
			if (!ModelState.IsValid) 
			{ 
				return View(category);
			}
			_categoryService.AddCategory(category);
			return RedirectToAction("ListCategory");
		}
		[HttpGet]		
		public IActionResult ListCategory(int pageSize, int pageNumber, string sortOrder)
		{
			if(pageNumber ==0)
			{
				pageNumber = 1;
				pageSize = 12;
			}
			ViewBag.IdSort = sortOrder == "id" ? "id_desc" : "id";
			ViewBag.CategorySort = sortOrder == "category" ? "category_desc" : "category";
			var categories = _categoryService.GetAllCategory(pageSize, pageNumber, sortOrder);
			return View(categories);
		}
		[HttpGet]		
		public IActionResult DeleteCategory(int id)
		{
			_categoryService.DeleteCategory(id);
			return RedirectToAction("ListCategory");
		}
		[HttpGet]		
		public IActionResult EditCategory(int id) 
		{
			var categoryToEdit = _categoryService.GetCategoryById(id);
			return View(categoryToEdit);
		}
		[HttpPost]	
		public IActionResult EditCategory(NewCategoryVm category)
		{
			if (ModelState.IsValid)
			{
				_categoryService.EditCategory(category);
				return RedirectToAction("ListCategory");
			}
			return View(category);
		}
	}
}

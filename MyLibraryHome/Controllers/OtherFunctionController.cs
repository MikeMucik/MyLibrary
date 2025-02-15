using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MyLibraryMVC.Application.Interfaces;
using MyLibraryMVC.Application.ViewModels.Category;
using MyLibraryMVC.Application.ViewModels.ManagerUser;
using MyLibraryMVC.Infrastructure;

namespace MyLibraryHome.Controllers
{
	[Authorize]
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
		public IActionResult AuthorsInfo(int pageSize = 12, int pageNumber= 1)
		{
			var model = _authorService.GetAllAuthors(pageSize, pageNumber );
			return View(model);
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
		public IActionResult ListCategory()
		{
			var categories = _categoryService.GetAllCategory();
			return View(categories);
		}
		[HttpGet]
		public IActionResult DeleteCategory(int id)
		{
			_categoryService.DeleteCategory(id);
			return RedirectToAction("ListCategory");
		}
	}
}

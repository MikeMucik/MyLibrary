using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Rendering;
using MyLibraryMVC.Application.ViewModels.Castegory;

namespace MyLibraryMVC.Application.Interfaces
{
	public interface ICategoryService
	{
		//ListCategoryVm GetAllCategories();
		List<SelectListItem> GetCategoryForSelectList();
	}
}

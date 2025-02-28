﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Rendering;
using MyLibraryMVC.Application.ViewModels.Castegory;
using MyLibraryMVC.Application.ViewModels.Category;

namespace MyLibraryMVC.Application.Interfaces
{
	public interface ICategoryService
	{
		void AddCategory(NewCategoryVm category);		
		List<SelectListItem> GetCategoryForSelectList();
		ListCategoryVm GetAllCategory(int pageSize, int pageNumber, string sortCategory);
		void DeleteCategory(int id);
		void EditCategory(NewCategoryVm category);
		NewCategoryVm GetCategoryById(int id);
	}
}

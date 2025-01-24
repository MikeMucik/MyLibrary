using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc.Rendering;
using MyLibraryMVC.Application.Interfaces;
using MyLibraryMVC.Application.ViewModels.Castegory;
using MyLibraryMVC.Domain.Interfaces;

namespace MyLibraryMVC.Application.Services
{
	public class CategoryService : ICategoryService
	{
		private readonly ICategoryRepo _categoryRepo;
		private readonly IMapper _mapper;
		public CategoryService(ICategoryRepo categoryRepo,
								IMapper mapper)
		{
			_categoryRepo = categoryRepo;
			_mapper = mapper;
		}

		//public ListCategoryVm GetAllCategories()
		//{
		//	var categories = _categoryRepo.GetAllCategories();
		//	var categoryVm = _mapper.Map<List<CategoryForListVm>>(categories);
		//	var categoryList = new ListCategoryVm
		//	{
		//		Categories = categoryVm,
		//	};
			
		//	return categoryList;
		//}

		public List<SelectListItem> GetCategoryForSelectList()
		{
			var categories = _categoryRepo.GetAllCategories();
			var categoriesVms = _mapper.Map<List<CategoryForListVm>>(categories);
			return categoriesVms.Select(c => new SelectListItem
			{
				Value = c.Id.ToString(),
				Text = c.Name
			}).ToList();
		}
	}
}

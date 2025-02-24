using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc.Rendering;
using MyLibraryMVC.Application.Interfaces;
using MyLibraryMVC.Application.ViewModels.Castegory;
using MyLibraryMVC.Application.ViewModels.Category;
using MyLibraryMVC.Domain.Interfaces;
using MyLibraryMVC.Domain.Model;

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
		public void AddCategory(NewCategoryVm category)
		{
			var categoryBase = _mapper.Map<Category>(category);
			_categoryRepo.AddCategory(categoryBase);
		}
		public void DeleteCategory(int id)
		{
			_categoryRepo.DeleteCategory(id);
		}
		public void EditCategory(NewCategoryVm categoryVm)
		{
			var category = _mapper.Map<Category>(categoryVm);
			_categoryRepo.EditCategory(category);
		}
		public ListCategoryVm GetAllCategory(int pageSize, int pageNumber, string sortOrder)
		{
			var categories = _categoryRepo.GetAllCategories()
							.Select(x => _mapper.Map<CategoryForListVm>(x))							
							.ToList();
			switch (sortOrder)
			{
				case "id":
					categories = [.. categories.OrderBy(c => c.Id)];
					break;
				case "id_desc":
					categories = [.. categories.OrderByDescending(c => c.Id)];
					break;
				case "category":
					categories = [.. categories.OrderBy(c=>c.Name)];
					break;
				case "category_desc":
					categories = [.. categories.OrderByDescending(c=>c.Name)];
					break;
				default: break;
			}
			var categoriesToShow = categories
				.Skip(pageSize*(pageNumber-1))
				.Take(pageSize)
				.ToList();
			var categoriesList = new ListCategoryVm
			{
				Categories = categoriesToShow,
				PageCurrent = pageNumber,
				PageSize = pageSize,
				TotalCount = categories.Count				
			};
			return categoriesList;
		}
		public NewCategoryVm GetCategoryById(int id)
		{
			var category = _categoryRepo.GetCategory(id);
			var categoryVm = _mapper.Map<NewCategoryVm>(category);
			return categoryVm;
		}
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

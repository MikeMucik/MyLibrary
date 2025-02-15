using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MyLibraryMVC.Domain.Interfaces;
using MyLibraryMVC.Domain.Model;

namespace MyLibraryMVC.Infrastructure.Repositories
{
	public class CategoryRepo : ICategoryRepo
	{
		private readonly Context _context;
		public CategoryRepo(Context context)
		{
			_context = context;
		}

		public void AddCategory(Category category)
		{
			_context.Categories.Add(category);
			_context.SaveChanges();
		}

		public void DeleteCategory(int id)
		{
			var category = _context.Categories.FirstOrDefault(c => c.Id == id);
			if (category != null) { _context.Categories.Remove(category); _context.SaveChanges(); }
		}

		public IEnumerable<Category> GetAllCategories()
		{
			return _context.Categories.ToList();
		}
	}
}

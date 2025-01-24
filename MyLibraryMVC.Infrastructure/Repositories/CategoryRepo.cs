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
		public IEnumerable<Category> GetAllCategories()
		{
			return _context.Categories.ToList();
		}
	}
}

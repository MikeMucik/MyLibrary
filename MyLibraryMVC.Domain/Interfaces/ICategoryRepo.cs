using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MyLibraryMVC.Domain.Model;

namespace MyLibraryMVC.Domain.Interfaces
{
	public interface ICategoryRepo
	{
		void AddCategory(Category category);
		void DeleteCategory(int id);
		void EditCategory(Category category);
		IEnumerable<Category> GetAllCategories();
		Category GetCategory(int id);
	}
}

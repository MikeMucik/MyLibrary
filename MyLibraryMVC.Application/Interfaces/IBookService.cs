using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MyLibraryMVC.Application.ViewModels.Book;

namespace MyLibraryMVC.Application.Interfaces
{
	public interface IBookService
	{
		int AddBook(NewBookVm model);		
		ListBooksVm GetAllBooks(int pageSize, int pageNumber, string searchString);
		BookDetailsVm GetBookDetails(int id);
	}
}

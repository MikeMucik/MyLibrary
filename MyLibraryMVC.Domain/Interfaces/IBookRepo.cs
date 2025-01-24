using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MyLibraryMVC.Domain.Model;

namespace MyLibraryMVC.Domain.Interfaces
{
	public interface IBookRepo
	{
		int AddBook(Book newBook);
		IQueryable<Book> GetAllBooks();
		Book GetBookDetails(int id);
	}
}

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
		//Book GetBookById(int id);
		Book GetBookDetails(int id);
		IQueryable<Book> GetBooksByDetais(int authorId, int categoryId,
			 int ageGroupId, int houseOfPublishingId);
		void UpdateBook(Book book);
	}
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MyLibraryMVC.Application.ViewModels.Book;
using MyLibraryMVC.Application.ViewModels.BookInfo;
using MyLibraryMVC.Application.ViewModels.Info;

namespace MyLibraryMVC.Application.Interfaces
{
	public interface IBookService
	{
		int AddBook(NewBookVm model);		
		ListBooksVm GetAllBooks(int pageSize, int pageNumber, string searchString);
		BookDetailsVm GetBookDetails(int id);
		FindBookVm FindBook(int pageSize, int pageNumber, int authorId,
			int categoryId, int ageGroupId, int houseOfPublishing, string searchString);
		NewBookVm GetBookToEdit(int id);
		int UpdateBook(NewBookVm model);
		NewBookInfoVm GetBookInfoByBookId(int bookId);
		NewInfoVm GetInfoByBookId(int bookId);
		void DeleteBook(int id);
		
	}
}

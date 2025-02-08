using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MyLibraryMVC.Domain.Model;

namespace MyLibraryMVC.Domain.Interfaces
{
	public interface IBookAuthorRepo
	{
		//void AddAuthor(BookAuthor author);
		void AddBookAuthor(BookAuthor bookAuthorBase);
		void DeleteAuthors (int bookId);
	}
}

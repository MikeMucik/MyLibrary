using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MyLibraryMVC.Domain.Interfaces;
using MyLibraryMVC.Domain.Model;

namespace MyLibraryMVC.Infrastructure.Repositories
{
	public class BookAuthorRepo : IBookAuthorRepo
	{
		private readonly Context _context;
		public BookAuthorRepo(Context context)
		{
			_context = context;
		}

		public void AddBookAuthor(BookAuthor bookAuthorBase)
		{
			_context.BooksAuthor.Add(bookAuthorBase);
			_context.SaveChanges();
		}

		public void DeleteAuthors(int bookId)
		{
			var entities = _context.BooksAuthor
				.Where(x=> x.BookId == bookId)
				.ToList();
			_context.BooksAuthor.RemoveRange(entities);
			_context.SaveChanges();
				
		}
		//public void AddAuthor(BookAuthor author)
		//{
		//	_context.BooksAuthor.Add(author);

		//}
	}
}

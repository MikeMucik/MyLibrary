using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using MyLibraryMVC.Domain.Interfaces;
using MyLibraryMVC.Domain.Model;

namespace MyLibraryMVC.Infrastructure.Repositories
{
	public class BookRepo : IBookRepo
	{
		private readonly Context _context;
		public BookRepo(Context context)
		{
			_context = context;
		}
		public IQueryable<Book> GetAllBooks()
		{
			return _context.Books;
		}

		public Book GetBookDetails(int id)
		{
			var book = _context.Books
				.Include(b => b.Authors)				
				.Include(b => b.BookInfo)
					.ThenInclude(i => i.AgeGroup)
				.Include(b => b.BookInfo)
					.ThenInclude(i => i.Category)
				.Include(b => b.PublishingInfo)
					.ThenInclude(i => i.PublishingHouse)
				.Include(b => b.PublishingInfo)
					.ThenInclude(i => i.CityOfPublishing)
				.FirstOrDefault(b => b.Id == id);
			return book;
		}
	}
}

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
		public int AddBook(Book newBook)
		{
			_context.Books.Add(newBook);
			_context.SaveChanges();
			return newBook.Id;
		}
		public void DeleteBook(int id)
		{
			var book = _context.Books.Find(id);
			_context.Books.Remove(book);
			_context.SaveChanges();
		}
		public IQueryable<Book> GetAllBooks()
		{
			return _context.Books;
		}
		//public object GetBookById(int id)
		//{
		//	var book = _context.Books
		//}
		public Book GetBookDetails(int id)
		{
			var book = _context.Books
				.Include(b => b.BookAuthors)
					.ThenInclude(i => i.Author)
				.Include(b => b.BookInfo)
					.ThenInclude(i => i.AgeGroup)
				.Include(b => b.Category)					
				.Include(b => b.PublishingInfo)
					.ThenInclude(i => i.PublishingHouse)
				.Include(b => b.PublishingInfo)
					.ThenInclude(i => i.CityOfPublishing)
				.FirstOrDefault(b => b.Id == id);
			return book;
		}
		public IQueryable<Book> GetBooksByDetais(int authorId, int categoryId, int ageGroupId, int houseOfPublishingId)
		{
			var books = _context.Books
		.Include(b => b.BookAuthors)
			.ThenInclude(ba => ba.Author)
		.Include(b => b.Category)			
		.Include(b => b.BookInfo)
			.ThenInclude(bi => bi.AgeGroup)
		.Include(b => b.PublishingInfo)
			.ThenInclude(bi => bi.PublishingHouse)
		.AsQueryable();
			if (authorId > 0)
			{
				books = books.Where(b => b.BookAuthors.Any(ba => ba.AuthorId == authorId));
			}
			if (categoryId > 0)
			{
				books = books.Where(b => b.CategoryId == categoryId);
			}
			if (ageGroupId > 0)
			{
				books = books.Where(b => b.BookInfo.AgeGroupId == ageGroupId);
			}
			if (houseOfPublishingId > 0)
			{
				books = books.Where(b => b.PublishingInfo.PublishingHouseId == houseOfPublishingId);
			}
			return books;
		}
		public void UpdateBook(Book book)
		{
			_context.Attach(book);
			_context.Entry(book).Property(nameof(book.Title)).IsModified = true;
			_context.Entry(book).Property(nameof(book.Description)).IsModified = true;
			_context.Entry(book).Property(nameof(book.CategoryId)).IsModified = true;
			if (book.BookInfo != null)
			{
				_context.Entry(book.BookInfo).Property(nameof(book.BookInfo.Illustration)).IsModified = true;
				_context.Entry(book.BookInfo).Property(nameof(book.BookInfo.Subtitle)).IsModified = true;
				_context.Entry(book.BookInfo).Property(nameof(book.BookInfo.NumberOfChapter)).IsModified = true;
				_context.Entry(book.BookInfo).Property(nameof(book.BookInfo.Binding)).IsModified = true;
				_context.Entry(book.BookInfo).Property(nameof(book.BookInfo.AgeGroupId)).IsModified = true;
				_context.Entry(book.BookInfo).Property(nameof(book.BookInfo.NumberOfPages)).IsModified = true;
				_context.Entry(book.BookInfo).Property(nameof(book.BookInfo.NumberOfChapter)).IsModified = true;
			}			
			if (book.PublishingInfo != null)
			{
				_context.Entry(book.PublishingInfo).Property(nameof(book.PublishingInfo.NumberOfPublishing)).IsModified = true;
				_context.Entry(book.PublishingInfo).Property(nameof(book.PublishingInfo.PublishingHouseId)).IsModified = true;
				_context.Entry(book.PublishingInfo).Property(nameof(book.PublishingInfo.CityOfPublishingId)).IsModified = true;
				_context.Entry(book.PublishingInfo).Property(nameof(book.PublishingInfo.PublishingDate)).IsModified = true;
				_context.Entry(book.PublishingInfo).Property(nameof(book.PublishingInfo.YearOfPublication)).IsModified = true;
			}
			_context.SaveChanges();
		}
	}
}

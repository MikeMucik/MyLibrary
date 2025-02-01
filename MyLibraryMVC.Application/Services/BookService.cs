using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using MyLibraryMVC.Application.Interfaces;
using MyLibraryMVC.Application.ViewModels.Book;
using MyLibraryMVC.Domain.Interfaces;
using MyLibraryMVC.Domain.Model;

namespace MyLibraryMVC.Application.Services
{
	public class BookService : IBookService
	{
		private readonly IBookRepo _bookRepo;
		private readonly IMapper _mapper;
		private readonly IAuthorService _authorService;
		private readonly IBookAuthorService _bookAuthorService;
		public BookService(IBookRepo bookRepo,
			IMapper mapper,
			IAuthorService authorService,
			IBookAuthorService bookAuthorService)
		{
			_bookRepo = bookRepo;
			_mapper = mapper;
			_authorService = authorService;
			_bookAuthorService = bookAuthorService;
		}
		public int AddBook(NewBookVm model)
		{
			var newBook = _mapper.Map<Book>(model);
			var toAddedBook = _bookRepo.AddBook(newBook);
			foreach (var item in model.Authors)
			{
				var authorId = _authorService.GetOrAddAuthor(item);
				var bookAuthorNew = new BookAuthor { AuthorId = authorId, BookId = toAddedBook };
				_bookAuthorService.AddBookAuthor(bookAuthorNew);
			}
			return toAddedBook;
		}
		public ListBooksVm GetAllBooks(int pageSize, int pageNumber, string searchString)
		{
			var books = _bookRepo.GetAllBooks()
				.OrderBy(x => x.Title)
				.Where(x => x.Title.StartsWith(searchString))
				.ProjectTo<BookForListVm>(_mapper.ConfigurationProvider)
				.ToList();
			var booksToShow = books
				.Skip(pageSize * (pageNumber - 1))
				.Take(pageSize)
				.ToList();
			var booksList = new ListBooksVm()
			{
				Books = booksToShow,
				PageNumber = pageNumber,
				PageSize = pageSize,
				SearchString = searchString,
				TotalCount = books.Count()
			};
			return booksList;
		}
		public FindBookVm FindBook(int pageSize, int pageNumber, int authorId,
			int categoryId, int ageGroupId, int houseOfPublishing)
		{
			var books = _bookRepo.GetBooksByDetais(authorId, categoryId, ageGroupId, houseOfPublishing)
				.OrderBy(x => x.Title)				
				.ProjectTo<BookForListVm>(_mapper.ConfigurationProvider)
				.ToList();
			var booksToShow = books
				.Skip(pageSize * (pageNumber - 1))
				.Take(pageSize)
				.ToList();
			var booksList = new FindBookVm()
			{
				Books = booksToShow,
				PageNumber = pageNumber,
				PageSize = pageSize,
				CategoryId = categoryId,
				AgeGroupId = ageGroupId,
				HouseOfPublishingId = houseOfPublishing ,
				TotalCount = books.Count()
			};
			return booksList;
		}
		public BookDetailsVm GetBookDetails(int id)
		{
			var book = _bookRepo.GetBookDetails(id);
			var bookDetails = _mapper.Map<BookDetailsVm>(book);
			return bookDetails;
		}
		public NewBookVm GetBookToEdit(int id)
		{
			var  book = _bookRepo.GetBookDetails(id);
			var bookVm = _mapper.Map<NewBookVm>(book);
			return bookVm;
		}
		public int UpdateBook(NewBookVm model)
		{

			Book book = _mapper.Map<Book>(model);
			book.BookAuthors.Clear();
			foreach (var item in model.Authors)
			{
				var authorId = _authorService.GetOrAddAuthor(item);
				var bookAuthorNew = new BookAuthor { AuthorId = authorId, BookId = model.Id };
				_bookAuthorService.AddBookAuthor(bookAuthorNew);
			}
			_bookRepo.UpdateBook(book);
			return model.Id;
		}
	}
}

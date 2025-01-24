using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using MyLibraryMVC.Application.Interfaces;
using MyLibraryMVC.Application.ViewModels.Author;
using MyLibraryMVC.Application.ViewModels.Book;
using MyLibraryMVC.Domain.Interfaces;
using MyLibraryMVC.Domain.Model;

namespace MyLibraryMVC.Application.Services
{
	public class BookService : IBookService
	{
		private readonly IBookRepo _bookRepo;
		private readonly IMapper _mapper;
		//private readonly IBookAuthorRepo _bookAuthorRepo;
		//private readonly IAuthorRepo _authorRepo;
		public BookService(IBookRepo bookRepo,
			IMapper mapper
			//,
			//IBookAuthorRepo bookAuthorRepo,
			//IAuthorRepo authorRepo
			)
		{
			_bookRepo = bookRepo;
			_mapper = mapper;
			//_bookAuthorRepo = bookAuthorRepo;
			//_authorRepo = authorRepo;
		}

		public int AddBook(NewBookVm model)
		{
			var newBook = _mapper.Map<Book>(model);			
			var toAddedBook = _bookRepo.AddBook(newBook);			
			return toAddedBook;
		}

		public ListBooksVm GetAllBooks(int pageSize, int pageNumber, string searchString)
		{
			var books = _bookRepo.GetAllBooks()
				.OrderBy(x=>x.Title)
				.Where(x=>x.Title.StartsWith(searchString))
				.ProjectTo<BookForListVm>(_mapper.ConfigurationProvider)
				.ToList();
			var booksToShow = books
				.Skip(pageSize *(pageNumber - 1))
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

		public BookDetailsVm GetBookDetails(int id)
		{
			var book = _bookRepo.GetBookDetails(id);
			var bookDetails = _mapper.Map<BookDetailsVm>(book);
			return bookDetails;
		}
	}
}

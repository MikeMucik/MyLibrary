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

namespace MyLibraryMVC.Application.Services
{
	public class BookService : IBookService
	{
		private readonly IBookRepo _bookRepo;
		private readonly IMapper _mapper;
		public BookService(IBookRepo bookRepo, IMapper mapper)
		{
			_bookRepo = bookRepo;
			_mapper = mapper;
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

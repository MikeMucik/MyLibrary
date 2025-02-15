using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using MyLibraryMVC.Application.Interfaces;
using MyLibraryMVC.Application.ViewModels.Book;
using MyLibraryMVC.Application.ViewModels.BookInfo;
using MyLibraryMVC.Application.ViewModels.City;
using MyLibraryMVC.Application.ViewModels.Info;
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
		private readonly ICityService _cityService;
		private readonly IHouseService _houseService;	
		private readonly ILoanRepo _loanRepo;		
		public BookService(IBookRepo bookRepo,
			IMapper mapper,
			IAuthorService authorService,
			IBookAuthorService bookAuthorService,
			ICityService cityService,
			IHouseService houseService,
			ILoanRepo loanRepo			
			)
		{
			_bookRepo = bookRepo;
			_mapper = mapper;
			_authorService = authorService;
			_bookAuthorService = bookAuthorService;
			_cityService = cityService;
			_houseService = houseService;
			_loanRepo = loanRepo;			
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
			foreach (var item in booksToShow)// tu musi być funkcja sprawdzająca i ustalająca czy książka jest dostępna
			{
				var today = DateTime.Today;
				item.IsLoan = _loanRepo.CheckLoan(item.Id, today);
			}
			var booksList = new ListBooksVm()
			{
				Books = booksToShow,
				CurrentPage = pageNumber,
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
				CurrentPage = pageNumber,
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
		public int AddBook(NewBookVm model)
		{
			if(model.NewInfo != null) { 
			if ((model.NewInfo.CityOfPublishingId == null || model.NewInfo.CityOfPublishingId == 0) 
					&& model.NewInfo.CityOfPublishingName != null)
			{					
					model.NewInfo.CityOfPublishingId = _cityService.AddCity(model.NewInfo.CityOfPublishingName);
			}
			if((model.NewInfo.PublishingHouseId == null || model.NewInfo.PublishingHouseId ==0) 
					&& model.NewInfo.PublishingHouseName != null) 
				{					
					model.NewInfo.PublishingHouseId = _houseService.AddHouse(model.NewInfo.PublishingHouseName);
				}
			}
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
		public NewBookVm GetBookToEdit(int id)
		{
			var  book = _bookRepo.GetBookDetails(id);
			var bookVm = _mapper.Map<NewBookVm>(book);
			var bookAuthorsIds = bookVm.Authors.Select(a=>a.Id).ToList();			
			return bookVm;
		}
		public int UpdateBook(NewBookVm model)
		{
			if (model.NewInfo != null)
			{
				if ((model.NewInfo.CityOfPublishingId == null || model.NewInfo.CityOfPublishingId == 0) 
					&& model.NewInfo.CityOfPublishingName != null)
				{
					model.NewInfo.CityOfPublishingId = _cityService.AddCity(model.NewInfo.CityOfPublishingName);
				}
				if ((model.NewInfo.PublishingHouseId == null || model.NewInfo.PublishingHouseId == 0)
					&& model.NewInfo.PublishingHouseName != null) 
				{
					model.NewInfo.PublishingHouseId = _houseService.AddHouse(model.NewInfo.PublishingHouseName);
				}
			}
			Book book = _mapper.Map<Book>(model);
			_bookAuthorService.DeleteAuthors(book.Id);
			foreach (var item in model.Authors)
			{
				var authorId = _authorService.GetOrAddAuthor(item);
				var bookAuthorNew = new BookAuthor { AuthorId = authorId, BookId = model.Id };
				_bookAuthorService.AddBookAuthor(bookAuthorNew);
			}
			_bookRepo.UpdateBook(book);
			return model.Id;
		}
		public NewBookInfoVm GetBookInfoByBookId(int bookId)
		{
			var book = _bookRepo.GetBookDetails(bookId);
			return new NewBookInfoVm
			{
				Id = book.BookInfo.Id,
				NumberOfChapter = book.BookInfo.NumberOfChapter,
				NumberOfPages = book.BookInfo.NumberOfPages,
				Illustration = book.BookInfo.Illustration,
				Binding = book.BookInfo.Binding,
				Subtitle = book.BookInfo.Subtitle,
				AgeGroupId = book.BookInfo.AgeGroupId
			};
		}
		public NewInfoVm GetInfoByBookId(int bookId)
		{
			var info = _bookRepo.GetBookDetails(bookId);
			return new NewInfoVm
			{
				Id = info.PublishingInfo.Id,
				YearOfPublication = info.PublishingInfo.YearOfPublication,
				PublishingHouseId = info.PublishingInfo.PublishingHouseId,
				NumberOfPublishing = info.PublishingInfo.NumberOfPublishing,
				PublishingDate = info.PublishingInfo.PublishingDate,
				CityOfPublishingId = info.PublishingInfo.CityOfPublishingId
			};
		}
		public void DeleteBook(int id)
		{
			_bookRepo.DeleteBook(id);
		}		
	}
}

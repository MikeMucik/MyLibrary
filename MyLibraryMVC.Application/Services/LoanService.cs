using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using MyLibraryMVC.Application.Interfaces;
using MyLibraryMVC.Application.ViewModels.Loan;
using MyLibraryMVC.Domain.Interfaces;
using MyLibraryMVC.Domain.Model;

namespace MyLibraryMVC.Application.Services
{
	public class LoanService : ILoanService
	{
		private readonly ILoanRepo _loanRepo;
		private readonly IMapper _mapper;
		private readonly IBookService _bookService;
		public LoanService(ILoanRepo loanRepo,
							IMapper mapper,
							IBookService bookService)
		{
			_loanRepo = loanRepo;
			_mapper = mapper;
			_bookService = bookService;
		}
		public int AddLoan(BookToLoanVm loan)
		{
			if (_loanRepo.IsLoan(loan.BookId, loan.LoanDate, loan.ReturnDate))
			{
				var loanData = _mapper.Map<Loan>(loan);
				var loanId = _loanRepo.AddLoan(loanData);
				return loanId;
			}
			return -1;
		}
		public int EditLoan(BookToLoanVm loanId)
		{
			if (_loanRepo.IsLoanEdit(loanId.Id, loanId.BookId, loanId.LoanDate, loanId.ReturnDate))
			{
				var loanData = _mapper.Map<Loan>(loanId);
				var loan = _loanRepo.EditLoan(loanData);
				return loan;
			}
			return -1;
		}
		public BookToLoanVm LoanToEdit(int loanId)
		{
			var loan = _loanRepo.GetLoanDetails(loanId);
			var loanVm = _mapper.Map<BookToLoanVm>(loan);
			return loanVm;
		}
		public BookToLoanVm BookToLoan(int bookId, string userId, string userName)
		{
			var book = _bookService.GetBookDetails(bookId);
			var loanVm = new BookToLoanVm
			{
				BookId = bookId,
				UserId = userId,
				BookTitle = book.Title,
				BookAuthor = book.Authors.First().Name + " " + book.Authors.First().SurName,
				UserName = userName
			};
			return loanVm;
		}
		public ListLoansVm GetAllLoan(int pageSize, int pageNumber, string sortOrder)
		{
			var loans = _loanRepo.GetAllLoans()
				.ProjectTo<LoanBookToListVm>(_mapper.ConfigurationProvider)
				//.OrderBy(x=>x.BookTitle)
				.ToList();
			foreach (var item in loans)// tu musi być funkcja sprawdzająca i ustalająca czy książka jest dostępna
			{
				var today = DateOnly.FromDateTime(DateTime.Now);
				item.IsLoan = _loanRepo.CheckLoan(item.BookId, today);
			}
			switch (sortOrder)
			{
				case "title":
					loans = loans.OrderBy(l => l.BookTitle).ToList();
					break;
				case "title_desc":
					loans = loans.OrderByDescending(l => l.BookTitle).ToList();
					break;
				case "author":
					loans = loans.OrderBy(l => l.BookAuthor).ToList();
					break;
				case "author_desc":
					loans = loans.OrderByDescending(l => l.BookAuthor).ToList();
					break;
				case "user":
					loans = loans.OrderBy(l => l.UserName).ToList();
					break;
				case "user_desc":
					loans = loans.OrderByDescending(l => l.UserName).ToList();
					break;
				case "loan":
					loans = loans.OrderBy(l => l.IsLoan).ToList();
					break;
				case "loan_desc":
					loans = loans.OrderByDescending(l => l.IsLoan).ToList();
					break;

				default:
					loans = loans.OrderBy(l => l.Id).ToList();
					break;
			}
			var loansToShow = loans
				.Skip(pageSize * (pageNumber - 1))
				.Take(pageSize)
				.ToList();

			var loanList = new ListLoansVm()
			{
				Loans = loansToShow,
				CurrentPage = pageNumber,
				PageSize = pageSize,
				TotalCount = loans.Count()
			};
			return loanList;
		}
		public ListLoansVm GetLoansByBook(int pageSize, int pageNumber, int bookId, string sortOrder)
		{
			var loans = _loanRepo.GetLoansByBookId(bookId)
				.ProjectTo<LoanBookToListVm>(_mapper.ConfigurationProvider)
				.ToList();
			foreach (var item in loans)// tu musi być funkcja sprawdzająca i ustalająca czy książka jest dostępna do zmiany
			{
				var today = DateOnly.FromDateTime(DateTime.Now);
				item.IsLoan = _loanRepo.CheckLoan(item.BookId, today);
			}
			switch (sortOrder)
			{
				case "user":
					loans = loans.OrderBy(l => l.UserName).ToList();
					break;
				case "user_desc":
					loans = loans.OrderByDescending(l => l.UserName).ToList();
					break;

				case "loanDate":
					loans = loans.OrderBy(l => l.LoanDate).ToList();
					break;
				case "loanDate_desc":
					loans = loans.OrderByDescending(l => l.LoanDate).ToList();
					break;
				case "loanReturn":
					loans = loans.OrderBy(l => l.ReturnDate).ToList();
					break;
				case "loanReturn_desc":
					loans = loans.OrderByDescending(l => l.ReturnDate).ToList();
					break;

				default:
					loans = loans.OrderBy(l => l.Id).ToList();
					break;
			}
			var loansToShow = loans
				.Skip(pageSize * (pageNumber - 1))
				.Take(pageSize)
				.ToList();
			var loanList = new ListLoansVm()
			{
				Loans = loansToShow,
				CurrentPage = pageNumber,
				PageSize = pageSize,
				TotalCount = loans.Count()
			};
			return loanList;
		}
		public void LoanDelete(int loanId)
		{
			_loanRepo.DeleteLaon(loanId);
		}
		public LoanDetailsVm LoanDetails(int loanId)
		{
			var loan = _loanRepo.GetLoanDetails(loanId);
			var loanVm = _mapper.Map<LoanDetailsVm>(loan);
			return loanVm;
		}
		public LoansByUserList GetLoansByUser(int pageSize, int pageNumber, string userId, string sortOrder)
		{
			var loans = _loanRepo.GetLoansByUser(userId)
				.ProjectTo<LoanByUserVm>(_mapper.ConfigurationProvider)
				.ToList();
			switch (sortOrder)
			{
				case "title":
					loans = loans.OrderBy(l => l.BookTitle).ToList();
					break;
				case "title_desc":
					loans = loans.OrderByDescending(l => l.BookTitle).ToList();
					break;
				case "loanDate":
					loans = loans.OrderBy(l=> l.LoanDate).ToList();
					break;
				case "loanDate_desc":
					loans = loans.OrderByDescending(l=>l.LoanDate).ToList();
					break;
				case "loanReturn":
					loans = loans.OrderBy(l => l.ReturnDate).ToList();
					break;
				case "loanReturn_desc":
					loans = loans.OrderByDescending(l => l.ReturnDate).ToList();
					break;

				default:
					loans = loans.OrderBy(l => l.Id).ToList();
					break;
			}
			var loansToShow = loans
				.Skip(pageSize * (pageNumber - 1))
				.Take(pageSize)
				.ToList();


			var loansVm = new LoansByUserList()
			{
				LoansList = loans,
				CurrentPage = pageNumber,
				PageSize = pageSize,
				TotalCount = loans.Count()
			};
			return loansVm;
		}
	}
}

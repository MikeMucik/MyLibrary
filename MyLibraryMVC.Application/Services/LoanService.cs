using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.AspNetCore.Identity;
using MyLibraryMVC.Application.Interfaces;
using MyLibraryMVC.Application.ViewModels.Loan;
using MyLibraryMVC.Domain.Interfaces;
using MyLibraryMVC.Domain.Model;
using MyLibraryMVC.Infrastructure;

namespace MyLibraryMVC.Application.Services
{
	public class LoanService : ILoanService
	{
		private readonly ILoanRepo _loanRepo;
		private readonly IMapper _mapper;
		private readonly IBookService _bookService;
		//private readonly UserManager<ApplicationUser> _userManager;
		public LoanService(ILoanRepo loanRepo,
							IMapper mapper,
							IBookService bookService
			//,UserManager<ApplicationUser> userManager
			)
		{
			_loanRepo = loanRepo;
			_mapper = mapper;
			_bookService = bookService;
			//_userManager = userManager;
		}
		public int AddLoan(BookToLoanVm loan)
		{
			if (IsAvailable(loan.BookId, loan.LoanDate, loan.ReturnDate))
			{
				var loanData = _mapper.Map<Loan>(loan);
				var loanId = _loanRepo.AddLoan(loanData);
				return loanId;
			}
			return -1;
		}

		public BookToLoanVm BookToLoan(int bookId, string userId, string userName)
		{
			//var userId = User. FindFirstValue(ClaimTypes.NameIdentifier);
			var book = _bookService.GetBookDetails(bookId);
			var modelLoanAdd = new BookToLoanVm
			{
				BookId = bookId,
				UserId = userId,
				BookTitle = book.Title,
				BookAuthor = book.Authors.First().Name + " " + book.Authors.First().SurName,
				UserName = userName,
			};
			return modelLoanAdd;
		}

		public ListLoansVm GetAllLoan(int pageSize, int pageNumber)
		{
			var loans = _loanRepo.GetAllLoans()
				.ProjectTo<LoanBookToListVm>(_mapper.ConfigurationProvider)
				.ToList();

			//foreach (var loan in loans)
			//{
			//	loan.UserName = _userManager.Users
			//		.FirstOrDefault(u => u.Id == loan.UserId)?
			//		.UserName ?? "Nienznany użytkownik";
			//}

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

		public bool IsAvailable(int bookId, DateTime loandDate, DateTime returnDate)
		{
			var isLoan = _loanRepo.IsAvailable(bookId, loandDate, returnDate);
			return isLoan;
		}

		public LoanDetailsVm LoanDetails(int loanId)
		{
			var loan = _loanRepo.GetLoanDetails(loanId);
			var loanVm = _mapper.Map<LoanDetailsVm>(loan);
			return loanVm;
		}
	}
}

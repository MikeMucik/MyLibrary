using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore.Metadata.Conventions;
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
			if (IsLoan(loan.BookId, loan.LoanDate, loan.ReturnDate))
			{
				var loanData = _mapper.Map<Loan>(loan);
				var loanId = _loanRepo.AddLoan(loanData);
				return loanId;
			}
			return -1;
		}
		public int EditLoan(BookToLoanVm loan)
		{
			if (IsLoan(loan.BookId, loan.LoanDate, loan.ReturnDate))
			{
				var loanData = _mapper.Map<Loan>(loan);
				var loanId = _loanRepo.EditLoan(loanData);
				return loanId;
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
		public ListLoansVm GetAllLoan(int pageSize, int pageNumber)
		{
			var loans = _loanRepo.GetAllLoans()
				.ProjectTo<LoanBookToListVm>(_mapper.ConfigurationProvider)
				.ToList();
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

		public ListLoansVm GetLoansByBook(int pageSize, int pageNumber, int bookId)
		{
			var loans = _loanRepo.GetLoansByBookId(bookId)
				.ProjectTo<LoanBookToListVm>(_mapper.ConfigurationProvider)
				.ToList();
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
		public bool IsLoan(int bookId, DateTime loandDate, DateTime returnDate)
		{
			var isLoan = _loanRepo.IsLoan(bookId, loandDate, returnDate);
			return isLoan;
		}

		public void LoanDelete(int loan)
		{
			_loanRepo.DeleteLaon(loan);
		}

		public LoanDetailsVm LoanDetails(int loanId)
		{
			var loan = _loanRepo.GetLoanDetails(loanId);
			var loanVm = _mapper.Map<LoanDetailsVm>(loan);
			return loanVm;
		}

		
	}
}

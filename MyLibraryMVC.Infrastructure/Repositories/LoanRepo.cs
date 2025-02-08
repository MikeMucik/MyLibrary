using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Identity.Client;
using MyLibraryMVC.Domain.Interfaces;
using MyLibraryMVC.Domain.Model;

namespace MyLibraryMVC.Infrastructure.Repositories
{
	public class LoanRepo : ILoanRepo
	{
		private readonly Context _context;
		//private readonly UserManager<ApplicationUser> _userManager;
		public LoanRepo(Context context
			//,UserManager<ApplicationUser> user
			)
		{
			_context = context;
			//_userManager = user;
		}

		public int AddLoan(Loan loanData)
		{
			_context.Loans.Add(loanData);
			_context.SaveChanges();
			return loanData.Id;
		}

		public IQueryable<Loan> GetAllLoans()
		{
			var loans = _context.Loans
				.Include(l => l.Book)
				//.Select(l => new LoanBookVm
				//{
				//	Id = l.Id,
				//	BookId = l.BookId,
				//	IsLoan = l.Book.BookInfo.IsLoan,
				//	UserId = l.UserID,
				//	UserName = _userManager.Users
				//	.Where(u => u.Id == l.UserID)
				//	.Select(u => u.UserName)
				//	.FirstOrDefault()
				//}).ToList()
				;

			return loans;
		}

		public Loan GetLoanDetails(int loanId)
		{
			var loan = _context.Loans
				.Include(b => b.Book)
					.ThenInclude(ba => ba.BookAuthors)
						.ThenInclude(bi=>bi.Author)
				.FirstOrDefault(a => a.Id == loanId);
			return loan;
		}

		public bool IsAvailable(int bookId, DateTime loandDate, DateTime returnDate)
		{
			return !_context.Loans.Any(l =>
			l.Book.Id == bookId &&
			l.ReturnDate <= loandDate &&
			l.LoanDate >= returnDate
				);
		}
		//public async Task<List<Loan>> GetAllLoansWithUsernamesA()
		//{
		//	var loans = await _context.Loans
		//		.Include(l => l.Book)
		//			.ThenInclude(b => b.BookInfo)
		//		.ToListAsync();

		//	foreach (var loan in loans)
		//	{
		//		var user = await _userManager.FindByIdAsync(loan.UserID);
		//		loan.UserID = user?.UserName ?? "Nieznany użytkownik";
		//	}

		//	return loans;
		//}
	}
}

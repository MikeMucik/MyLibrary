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

		public bool CheckLoan(int bookId, DateTime today)
		{

			var IsLoan = _context.Loans.Any(l =>
			l.Book.Id == bookId &&
			l.LoanDate <= today &&
			l.ReturnDate >= today
				);
			return IsLoan;
		}
		public void DeleteLaon(int loanId)
		{
			var loan = _context.Loans.Find(loanId);
			_context.Loans.Remove(loan);
			_context.SaveChanges();
		}
		public int EditLoan(Loan loanData)
		{
			_context.Attach(loanData);
			_context.Entry(loanData).Property(nameof(loanData.LoanDate));
			_context.Entry(loanData).Property(nameof(loanData.ReturnDate));
			_context.SaveChanges();
			return loanData.Id;
		}

		public IQueryable<Loan> GetAllLoans()
		{
			var loans = _context.Loans
				.Include(l => l.Book);
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

		public IQueryable<Loan> GetLoansByBookId(int bookId)
		{
			var loans = _context.Loans
				.Include(l => l.Book)
					.ThenInclude(ba => ba.BookAuthors)
						.ThenInclude(bi => bi.Author)
				.Where(a => a.BookId == bookId);
			return loans;
		}

		public bool IsLoan(int bookId, DateTime loanDate, DateTime returnDate)
		{
			var boolValue= !_context.Loans.Any(l =>
			bookId == l.BookId &&
			!(loanDate >= l.ReturnDate || returnDate <= l.LoanDate));
			return boolValue;
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

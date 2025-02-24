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
	public class LoanRepo : ILoanRepo
	{
		private readonly Context _context;

		public LoanRepo(Context context)
		{
			_context = context;
		}

		public int AddLoan(Loan loanData)
		{
			_context.Loans.Add(loanData);
			_context.SaveChanges();
			return loanData.Id;
		}
		public bool CheckLoan(int bookId, DateOnly today)
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
			if (loanId > 0)
			{
				var loan = _context.Loans.Find(loanId);
				_context.Loans.Remove(loan);
				_context.SaveChanges();
			}
		}
		public int EditLoan(Loan loanData)
		{
			_context.Attach(loanData);
			_context.Entry(loanData).Property(nameof(loanData.LoanDate)).IsModified = true;
			_context.Entry(loanData).Property(nameof(loanData.ReturnDate)).IsModified = true;
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
						.ThenInclude(bi => bi.Author)
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
		public IQueryable<Loan> GetLoansByUser(string userId)
		{
			var loans = _context.Loans
				.Include(l => l.Book)
				.Where(l => l.UserId == userId);
			return loans;
		}
		public bool IsLoan(int bookId, DateOnly loanDate, DateOnly returnDate)
		{
			var boolValue = !_context.Loans.Any(l =>
			bookId == l.BookId &&
			!(loanDate >= l.ReturnDate || returnDate <= l.LoanDate));
			return boolValue;
		}
		public bool IsLoanEdit(int loanId, int bookId, DateOnly loanDate, DateOnly returnDate)
		{
			var boolValue = !_context.Loans
				.Where(l=>l.Id != loanId)
				.Any(l =>			
			bookId == l.BookId &&
			!(loanDate >= l.ReturnDate || returnDate <= l.LoanDate));
			return boolValue;
		}		
	}
}

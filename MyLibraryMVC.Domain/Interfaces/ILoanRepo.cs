using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MyLibraryMVC.Domain.Model;

namespace MyLibraryMVC.Domain.Interfaces
{
	public interface ILoanRepo
	{
		int AddLoan(Loan loanData);
		bool CheckLoan(int bookId, DateOnly today);
		void DeleteLaon(int loanId);
		int EditLoan(Loan loanData);
		IQueryable<Loan> GetAllLoans();
		Loan GetLoanDetails(int loanId);
		IQueryable<Loan> GetLoansByBookId(int bookId);
		IQueryable<Loan> GetLoansByUser(string userId);
		bool IsLoan(int bookId, DateOnly loandDate, DateOnly returnDate);
		bool IsLoanEdit(int loanId, int bookId, DateOnly loanDate, DateOnly returnDate);
	}
}

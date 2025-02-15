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
		bool CheckLoan(int bookId, DateTime today);
		void DeleteLaon(int loanId);
		int EditLoan(Loan loanData);
		IQueryable<Loan> GetAllLoans();
		Loan GetLoanDetails(int loanId);
		IQueryable<Loan> GetLoansByBookId(int bookId);
		bool IsLoan(int bookId, DateTime loandDate, DateTime returnDate);
	}
}

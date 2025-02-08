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
		IQueryable<Loan> GetAllLoans();
		Loan GetLoanDetails(int loanId);
		bool IsAvailable(int bookId, DateTime loandDate, DateTime returnDate);
	}
}

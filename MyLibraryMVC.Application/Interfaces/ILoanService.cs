using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MyLibraryMVC.Application.ViewModels.Loan;
using MyLibraryMVC.Domain.Interfaces;
using MyLibraryMVC.Domain.Model;

namespace MyLibraryMVC.Application.Interfaces
{
	public interface ILoanService
	{
		//string? AddBookLoan(int id);
		int AddLoan(BookToLoanVm loan);
		int EditLoan(BookToLoanVm loan);
		BookToLoanVm LoanToEdit(int loanId);
		BookToLoanVm BookToLoan(int bookId, string userId, string userName);
		ListLoansVm GetAllLoan(int pageSize, int pagenumber);
		bool IsLoan(int bookId, DateTime loandDate, DateTime returnDate);
		LoanDetailsVm LoanDetails(int loanId);
		ListLoansVm GetLoansByBook(int pageSize, int pageNumber, int bookId);
		void LoanDelete(int Loan);
		

		//bool IsLoan(int bookId, DateTime today);
	}
}

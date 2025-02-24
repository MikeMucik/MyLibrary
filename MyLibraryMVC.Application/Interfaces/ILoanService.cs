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
		int AddLoan(BookToLoanVm loan);
		int EditLoan(BookToLoanVm loanId);
		BookToLoanVm LoanToEdit(int loanId);
		BookToLoanVm BookToLoan(int bookId, string userId, string userName);
		ListLoansVm GetAllLoan(int pageSize, int pagenumber, string sortOrder);		
		LoanDetailsVm LoanDetails(int loanId);
		ListLoansVm GetLoansByBook(int pageSize, int pageNumber, int bookId, string sortOrder);
		void LoanDelete(int loanId);
		LoansByUserList GetLoansByUser(int pageSize, int pageNumber, string userId, string sortOrder);
	}
}

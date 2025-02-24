using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using MyLibraryMVC.Application.Interfaces;
using MyLibraryMVC.Application.ViewModels.Loan;
using MyLibraryMVC.Domain.Model;

namespace MyLibraryHome.Controllers
{
	[Authorize]
	[AutoValidateAntiforgeryToken]
	public class LoanController : Controller
	{
		private readonly ILoanService _loanService;
		private readonly IBookService _bookService;

		public LoanController(ILoanService loanService
								, IBookService bookService)
		{
			_loanService = loanService;
			_bookService = bookService;
		}
		[HttpGet]		
		public IActionResult LoanIndex(int pageSize = 12, int pageNumber = 1, string sortOrder = "")
		{
			ViewBag.TitleSortParam = sortOrder == "title" ? "title_desc" : "title";
			ViewBag.UserSortParam = sortOrder == "user" ? "user_desc" : "user";
			ViewBag.AuthorSortParam = sortOrder == "author" ? "author_desc" : "author";
			ViewBag.LoanSortParam = sortOrder == "loan" ? "loan_desc" : "loan";
			var modelLoan = _loanService.GetAllLoan(pageSize, pageNumber, sortOrder);
			return View(modelLoan);
		}
		[HttpGet]		
		public IActionResult BookLoans(int pageSize = 12, int pageNumber = 1, int bookId = 0, string sortOrder = "")
		{			
			ViewBag.UserSortParam = sortOrder == "user" ? "user_desc" : "user";
			ViewBag.LoanDateSortParam = sortOrder == "loanDate" ? "loanDate_desc" : "loanDate";
			ViewBag.LoanReturnSortParam = sortOrder == "loanReturn" ? "loanReturn_desc" : "loanReturn";
			var modelLoan = _loanService.GetLoansByBook(pageSize, pageNumber, bookId, sortOrder);
			if (modelLoan.Loans.Count == 0)
			{
				var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
				var userName = User.FindFirstValue(ClaimTypes.Name);
				return RedirectToAction("LoanAdd", "Loan", new { BookId = bookId, UserId = userId, UserName = userName });
			}
			return View(modelLoan);
		}		
		[HttpGet]		
		public IActionResult LoanDetails(int loanId)
		{
			var loan = _loanService.LoanDetails(loanId);
			return View(loan);
		}
		[HttpGet]		
		public IActionResult LoanAdd(int bookId)
		{
			var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
			var userName = User.FindFirstValue(ClaimTypes.Name);
			var modelLoanAdd = _loanService.BookToLoan(bookId, userId, userName);
			return View(modelLoanAdd);
		}
		[HttpPost]		
		public IActionResult LoanAdd(BookToLoanVm loan)
		{
			if (ModelState.IsValid)
			{
				var loanStatus = _loanService.AddLoan(loan);
				if (loanStatus == -1)
				{
					ModelState.AddModelError("", "Książka jest wypożyczona w podanym terminie");
					return View(loan);
				}
			}
			return RedirectToAction("BookLoans", "Loan", new { bookId = loan.BookId });
		}
		[HttpGet]		
		public IActionResult LoanEdit(int loanId)
		{
			var modelLoanAdd = _loanService.LoanToEdit(loanId);
			return View(modelLoanAdd);
		}
		[HttpPost]	
		public IActionResult LoanEdit(BookToLoanVm loan)
		{
			if (ModelState.IsValid)
			{
				var loadStatus = _loanService.EditLoan(loan);
				if (loadStatus == -1)
				{
					ModelState.AddModelError("", "Książka jest wypożyczona");
					return View(loan);
				}
			}
			return RedirectToAction("BookLoans", "Loan", new { bookId = loan.BookId });
		}
		[HttpGet]		
		public IActionResult LoanDelete(int loanId)
		{
			_loanService.LoanDelete(loanId);
			return RedirectToAction("LoanIndex", "Loan");
		}
		[HttpGet]		
		public IActionResult ShowLoansByUser(int pageSize, int pageNumber, string userId, string sortOrder)
		{
			pageSize = 12;
			if(pageNumber == 0)
			{
				pageNumber = 1;
			}
			ViewBag.TitleSortParam = sortOrder == "title" ? "title_desc" : "title";
			ViewBag.LoanDateSortParam = sortOrder == "loanDate" ? "loanDate_desc" : "loanDate";
			ViewBag.LoanReturnSortParam = sortOrder == "loanReturn" ? "loanReturn_desc" : "loanReturn";
			var loans = _loanService.GetLoansByUser(pageSize,pageNumber, userId, sortOrder);
			return View(loans);
		}
	}
}

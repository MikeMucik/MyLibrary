using System.Security.Claims;
using Microsoft.AspNetCore.Mvc;
using MyLibraryMVC.Application.Interfaces;
using MyLibraryMVC.Application.ViewModels.Loan;

namespace MyLibraryHome.Controllers
{
	public class LoanController : Controller
	{
		private readonly ILoanService _loanService;
		private readonly IBookService _bookService;
		
		public LoanController(ILoanService loanService
								,IBookService bookService)
		{
			_loanService = loanService;
			_bookService = bookService;
		}
		public IActionResult LoanIndex(int pageSize = 12, int pageNumber = 1)
		{			
			var modelLoan = _loanService.GetAllLoan(pageSize, pageNumber);
			return View(modelLoan);
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
				var loadStatus = _loanService.AddLoan(loan);
				if (loadStatus == -1)
				{
					ModelState.AddModelError("", "Książka jest wypożyczona");
					return View();
				}
			}
			return RedirectToAction("LoanIndex");
		}
		[HttpGet]
		public IActionResult LoanDetails(int loanId)
		{
			var loan = _loanService.LoanDetails(loanId);
			return View(loan);
		}
	}
}

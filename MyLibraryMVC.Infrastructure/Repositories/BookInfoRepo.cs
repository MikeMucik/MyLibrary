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
	public class BookInfoRepo : IBookInfoRepo
	{
		private readonly Context _context;
		public BookInfoRepo(Context context)
		{
			_context = context;
		}
		//public int AddBookInfo(BookInfo newBookInfo)
		//{
		//	_context.BookInfo.Add(newBookInfo);
		//	return newBookInfo.Id;
		//}
		public void ChangeIsAvailable(int bookId, bool checkedLoan)
		{
			var book = _context.Books.Find(bookId)
				.BookInfo.IsLoan = checkedLoan;
		}
	}
}

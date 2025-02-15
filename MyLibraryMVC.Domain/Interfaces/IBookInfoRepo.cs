using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MyLibraryMVC.Domain.Model;

namespace MyLibraryMVC.Domain.Interfaces
{
	public interface IBookInfoRepo
	{
		//int AddBookInfo(BookInfo newBookInfo);
		void ChangeIsAvailable(int bookId, bool checkedLoan);
	}
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore.Metadata.Conventions;
using MyLibraryMVC.Application.ViewModels.BookInfo;
using MyLibraryMVC.Domain.Model;

namespace MyLibraryMVC.Application.Interfaces
{
	public interface IBookInfoService
	{
		void ChangeIsAvailable(int bookId, bool checkedLoan);

	}
}

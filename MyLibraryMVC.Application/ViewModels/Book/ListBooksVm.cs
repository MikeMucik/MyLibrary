using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyLibraryMVC.Application.ViewModels.Book
{
	public class ListBooksVm
	{
		public List<BookForListVm>? Books { get; set; }
		public int CurrentPage { get; set; }
		public int PageNumber { get; set; }
		public int PageSize { get; set; }
		public int TotalCount { get; set; }
		public string? SearchString { get; set; }
	}
}

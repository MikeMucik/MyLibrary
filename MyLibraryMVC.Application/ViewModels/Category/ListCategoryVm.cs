using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MyLibraryMVC.Application.ViewModels.Category;

namespace MyLibraryMVC.Application.ViewModels.Castegory
{
	public class ListCategoryVm
	{
		public List<CategoryForListVm> Categories { get; set; }
		public int PageSize { get; set; }
		public int PageNumber { get; set; }
		public int PageCurrent {  get; set; }
		public int TotalCount { get; set; }

	}
}

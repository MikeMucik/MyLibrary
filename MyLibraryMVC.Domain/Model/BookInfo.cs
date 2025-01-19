using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyLibraryMVC.Domain.Model
{
	public class BookInfo
	{
		public int Id { get; set; }
		public int NumberOfPages { get; set; }
		public int NumberOfChapter { get; set; }
		public bool Illustration { get; set; }//
		public string? Binding { get; set; }
		public string? Subtitle { get; set; }
		public required int CategoryId { get; set; }
		public required virtual Category Category { get; set; }
		public int AgeGroupId { get; set; }
		public virtual AgeGroup? AgeGroup { get; set; }
		public string? Description { get; set; }
		public bool IsLoan { get; set; } = false;
	}
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyLibraryMVC.Domain.Model
{
	public class Author
	{
		public int Id { get; set; }
		public string? Name { get; set; }
		public string? SurName { get; set; }
		public string? RealName { get; set; }
		public string? RealSurName { get; set; } 
		public DateOnly? DateOfBirth { get; set; }
		public virtual ICollection<BookAuthor> BookAuthors { get; set; } = new List<BookAuthor>();
	}
}

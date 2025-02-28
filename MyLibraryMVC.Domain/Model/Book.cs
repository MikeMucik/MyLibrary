﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyLibraryMVC.Domain.Model
{
	public class Book
	{
		public int Id { get; set; }
		public required string Title { get; set; }
		public required virtual ICollection<BookAuthor> BookAuthors { get; set; }	= new List<BookAuthor>();			
		public int? PublishingInfoId { get; set; }
		public virtual Info? PublishingInfo { get; set; }		
		public ICollection<Loan>? Loan { get; set; }
		public int? BookInfoId { get; set; }
		public required virtual BookInfo BookInfo { get; set; }

		public required int CategoryId { get; set; }
		public required virtual Category Category { get; set; }
		public string? Description { get; set; }

	}
}

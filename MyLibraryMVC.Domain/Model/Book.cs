using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyLibraryMVC.Domain.Model
{
	public class Book
	{
		public int Id { get; set; }
		public string Title { get; set; }
		public virtual ICollection<Author> Authors { get; set; }
		public string Subtitle { get; set; }
		public int CategoryId { get; set; }
		public virtual Category Category { get; set; }
		public int AgeGroupId { get; set; }
		public virtual AgeGroup AgeGroup { get; set; }
		public int PublishingInfoId { get; set; }
		public virtual Info PublishingInfo { get; set; }
		public string Description { get; set; }
		public bool IsBorrowed { get; set; } = false;
		public ICollection<Loan> Loan { get; set; }

	}
}

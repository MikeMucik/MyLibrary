using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyLibraryMVC.Domain.Model
{
	public class Loan
	{
		public int Id { get; set; }
		public required int BookId { get; set; }
		public required virtual Book Book { get; set; }			
		public required string UserId { get; set; }		
		public required string UserName { get; set; }		
		public DateOnly LoanDate { get; set; }//
		public DateOnly? ReturnDate { get; set; }//
	}	
}

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
		public int BookId { get; set; }
		public virtual Book Book { get; set; }
		public string UserID { get; set; }
		//public IUser User { get; set; }
		public DateTime LoanTime { get; set; }
		public DateTime? ReturnDate { get; set; }
	}
	//public interface IUser 
	//{
	//	string UserID { get; set; }
	//	string UserName { get; set; }

	//}
}

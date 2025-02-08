using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using MyLibraryMVC.Application.Mapping;

namespace MyLibraryMVC.Application.ViewModels.Loan
{
	public class BookToLoanVm : IMapFrom<Domain.Model.Loan>
	{
		public int Id { get; set; }
		public int BookId { get; set; }
		public string BookTitle { get; set; }
		public string BookAuthor { get; set; }
		public string UserId { get; set; }
		public string UserName { get; set; }
		public DateTime LoanDate { get; set; }
		public DateTime ReturnDate { get; set; }
		public void Mapping(Profile profile)
		{
			profile.CreateMap<BookToLoanVm, Domain.Model.Loan>()
				.ForMember(dest=> dest.UserID, opt=>opt.MapFrom(l=>l.UserId));
		}
	}
}

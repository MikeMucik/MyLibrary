using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using MyLibraryMVC.Application.Mapping;

namespace MyLibraryMVC.Application.ViewModels.Loan
{
	public class LoanBookToListVm :IMapFrom<Domain.Model.Loan>
	{
		public int Id { get; set; }
		public int BookId { get; set; }
		public string BookTitle {  get; set; }
		public string BookAuthor { get; set; }
		public bool IsLoan { get; set; }
		public string UserId { get; set; }
		public string UserName { get; set; }
		public void Mapping(Profile profile)
		{
			profile.CreateMap<Domain.Model.Loan, LoanBookToListVm>()
				.ForMember(b=>b.IsLoan, opt=>opt.MapFrom(a=>a.Book.BookInfo.IsLoan))
				.ForMember(b=>b.BookTitle, opt=>opt.MapFrom(a=>a.Book.Title))
				.ForMember(b=>b.UserId, opt=>opt.MapFrom(a=>a.UserID))
				.ForMember(b=>b.BookAuthor, opt=>opt.MapFrom(
					a=>a.Book.BookAuthors.First().Author.Name +" "+ a.Book.BookAuthors.First().Author.SurName));

		}
	}
}

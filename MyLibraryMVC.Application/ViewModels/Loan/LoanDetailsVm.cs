using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using MyLibraryMVC.Application.Mapping;

namespace MyLibraryMVC.Application.ViewModels.Loan
{
	public class LoanDetailsVm :IMapFrom<Domain.Model.Loan>
	{
		public int Id { get; set; }
		public string BookTitle { get; set; }
		public string BookAuthor { get; set; }
		public string UserId { get; set; }
		public string UserName { get; set; }
		public DateTime LoanDate { get; set; }
		public DateTime ReturnDate { get; set; }
		public void Mapping(Profile profile)
		{
			profile.CreateMap<Domain.Model.Loan, LoanDetailsVm>()
				.ForMember(dest => dest.UserId, opt => opt.MapFrom(l => l.UserID))
				.ForMember(dest => dest.BookTitle, opt => opt.MapFrom(l => l.Book.Title))
				.ForMember(dest => dest.BookAuthor, opt => opt.MapFrom(l =>	string.Join(", ", 
				l.Book.BookAuthors.Select(la=>la.Author.Name + " " + la.Author.SurName))));
		}
	}
}

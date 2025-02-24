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
		public DateOnly LoanDate { get; set; }
		public DateOnly ReturnDate { get; set; }
		public void Mapping(Profile profile)
		{
			profile.CreateMap<BookToLoanVm, Domain.Model.Loan>();

			profile.CreateMap<Domain.Model.Loan, BookToLoanVm>()				
				.ForMember(b => b.BookTitle, opt => opt.MapFrom(a => a.Book.Title))
				.ForMember(b => b.UserId, opt => opt.MapFrom(a => a.UserId))
				.ForMember(b => b.LoanDate, opt => opt.MapFrom(a => a.LoanDate))
				.ForMember(b => b.ReturnDate, opt => opt.MapFrom(a => a.ReturnDate))
				.ForMember(dest => dest.BookAuthor, opt => opt.MapFrom(l => string.Join(", ",
				l.Book.BookAuthors.Select(la => la.Author.Name + " " + la.Author.SurName))));
		}
	}
}

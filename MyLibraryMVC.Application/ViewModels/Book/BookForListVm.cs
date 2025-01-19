using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using MyLibraryMVC.Application.Mapping;
using MyLibraryMVC.Application.ViewModels.Author;
using MyLibraryMVC.Domain.Model;

namespace MyLibraryMVC.Application.ViewModels.Book
{
	public class BookForListVm : IMapFrom<Domain.Model.Book>
	{
		public int Id { get; set; }
		public required string Title { get; set; }
		public required List<AuthorVm> Authors { get; set; }
		public required string Category { get; set; }
		public string? AgeGroup { get; set; }
		public required bool IsLoan { get; set;}
		public void Mapping(Profile profile)
		{
			profile.CreateMap<Domain.Model.Book, BookForListVm>()
				.ForMember(b=>b.Title, opt => opt.MapFrom(a=>a.Title))
				.ForMember(b=>b.Authors, opt => opt.MapFrom(a=>a.Authors))
				.ForMember(b=>b.Category, opt=>opt.MapFrom(a=>a.BookInfo.Category.Name))
				.ForMember(b=>b.AgeGroup, opt=>opt.MapFrom(a=>a.BookInfo.AgeGroup.Name))
				.ForMember(b=>b.IsLoan, opt=>opt.MapFrom(a=>a.BookInfo.IsLoan));
		}
	}
}

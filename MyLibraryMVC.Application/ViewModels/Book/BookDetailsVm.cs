using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using MyLibraryMVC.Application.Mapping;
using MyLibraryMVC.Application.ViewModels.Author;

namespace MyLibraryMVC.Application.ViewModels.Book
{
	public class BookDetailsVm : IMapFrom<Domain.Model.Book>
	{
		public int Id { get; set; }
		public required string Title { get; set; }
		public required List<AuthorVm> Authors { get; set; } = new List<AuthorVm>();
		public required string Category { get; set; }
		public string? AgeGroup { get; set; }
		public int? NumberOfPages { get; set; }
		public int? NumberOfChapter { get; set; }
		public bool Illustration { get; set; }//
		public string? Binding { get; set; }
		public int? YearOfPublication { get; set; }
		public string? PublishingHouse { get; set; }
		public int? NumberOfPublishing { get; set; }
		public DateOnly? PublishingDate { get; set; }
		public string? CityOfPublishing { get; set; }
		public string? Description { get; set; }
		public string? Subtitle { get; set; }
		public void Mapping(Profile profile)
		{
			profile.CreateMap<Domain.Model.Book, BookDetailsVm>()
				.ForMember(b => b.Authors, opt => opt.MapFrom(a => a.BookAuthors))
				.ForMember(b => b.Category, opt => opt.MapFrom(a => a.Category.Name))
				//.ForMember(b=>b.Category, opt=>opt.MapFrom(a=>a.Category))
				.ForMember(b => b.AgeGroup, opt => opt.MapFrom(a => a.BookInfo.AgeGroup.Name))
				.ForMember(b => b.NumberOfPages, opt => opt.MapFrom(a => a.BookInfo.NumberOfPages))
				.ForMember(b => b.NumberOfChapter, opt => opt.MapFrom(a => a.BookInfo.NumberOfChapter))
				.ForMember(b => b.Illustration, opt => opt.MapFrom(a => a.BookInfo.Illustration))
				.ForMember(b => b.Binding, opt => opt.MapFrom(a => a.BookInfo.Binding))
				.ForMember(b => b.YearOfPublication, opt => opt.MapFrom(a => a.PublishingInfo.YearOfPublication))
				.ForMember(b => b.PublishingHouse, opt => opt.MapFrom(a => a.PublishingInfo.PublishingHouse.Name))
				.ForMember(b => b.NumberOfPublishing, opt => opt.MapFrom(a => a.PublishingInfo.NumberOfPublishing))
				.ForMember(b => b.PublishingDate, opt => opt.MapFrom(a => a.PublishingInfo.PublishingDate))
				.ForMember(b => b.CityOfPublishing, opt => opt.MapFrom(a => a.PublishingInfo.CityOfPublishing.Name))				
				.ForMember(b=>b.Description, opt=>opt.MapFrom(a=>a.Description))
				.ForMember(b => b.Subtitle, opt => opt.MapFrom(a => a.BookInfo.Subtitle));
		}
	}
}

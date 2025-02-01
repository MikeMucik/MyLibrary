using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using MyLibraryMVC.Application.Mapping;
using MyLibraryMVC.Application.ViewModels.Author;
using MyLibraryMVC.Application.ViewModels.BookInfo;
using MyLibraryMVC.Application.ViewModels.Info;
using MyLibraryMVC.Domain.Model;

namespace MyLibraryMVC.Application.ViewModels.Book
{
	public class NewBookVm : IMapFrom<Domain.Model.Book>
		
	{
		public int Id { get; set; }
		public string Title { get; set; }		
		public List<NewAuthorVm> Authors { get; set; } = new List<NewAuthorVm>();	
		public int CategoryId { get; set; }
		public string Description { get; set; }
		public NewBookInfoVm? BookInfo { get; set; }
		public NewInfoVm? NewInfo { get; set; }				
		public void Mapping(Profile profile)
		{
			profile.CreateMap<NewBookVm, Domain.Model.Book>()				
				.ForMember(a => a.BookAuthors, dest => dest.Ignore())
				.ForPath(a=>a.BookInfo.CategoryId, dest=>dest.MapFrom(b => b.CategoryId))
				.ForPath(a => a.BookInfo.Description, dest => dest.MapFrom(b => b.Description))
				.ForMember(a=>a.BookInfo, dest=> dest.MapFrom(b=>b.BookInfo))
				.ForMember(a=>a.PublishingInfo, dest=> dest.MapFrom(b=>b.NewInfo))
				
				;

			profile.CreateMap<Domain.Model.Book, NewBookVm>()
				.ForMember(a => a.BookInfo, b => b.MapFrom(c => c.BookInfo))
				.ForMember(a => a.NewInfo, b => b.MapFrom(c => c.PublishingInfo))
				.ForMember(a => a.Authors, b => b.MapFrom(c => c.BookAuthors))
				.ForMember(a=>a.CategoryId, b=>b.MapFrom(c=>c.BookInfo.CategoryId))
				.ForMember(a=>a.Description, b=>b.MapFrom(c=>c.BookInfo.Description));
				
		}
	}
}

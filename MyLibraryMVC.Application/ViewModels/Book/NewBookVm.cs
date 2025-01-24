using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using MyLibraryMVC.Application.Mapping;
using MyLibraryMVC.Application.ViewModels.Author;
using MyLibraryMVC.Application.ViewModels.BookInfo;
using MyLibraryMVC.Domain.Model;

namespace MyLibraryMVC.Application.ViewModels.Book
{
	public class NewBookVm : IMapFrom<Domain.Model.Book>
		
	{
		public int Id { get; set; }
		public string Title { get; set; }
		public List<AuthorVm> Authors { get; set; } = new List<AuthorVm>();	
		public int CategoryId { get; set; }
		public string Description { get; set; }
		public NewBookInfoVm? BookInfo { get; set; } = new NewBookInfoVm();
		public void Mapping(Profile profile)
		{
			profile.CreateMap<NewBookVm, Domain.Model.Book>()
				.ForMember(a => a.BookAuthors, dest => dest.MapFrom(b => b.Authors))
				.ForPath(a=>a.BookInfo.CategoryId, dest=>dest.MapFrom(b => b.CategoryId))
				.ForPath(a => a.BookInfo.Description, dest => dest.MapFrom(b => b.Description))
				.ForMember(a=>a.BookInfo, dest=> dest.MapFrom(b=>b.BookInfo));			
		}
	}
}

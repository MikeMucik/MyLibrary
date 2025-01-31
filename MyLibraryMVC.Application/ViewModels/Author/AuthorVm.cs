using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using MyLibraryMVC.Application.Mapping;

namespace MyLibraryMVC.Application.ViewModels.Author
{
	public class AuthorVm :
							IMapFrom<Domain.Model.BookAuthor>,
							IMapFrom<Domain.Model.Author>
	{
		public int? Id { get; set; }
		public string? Name { get; set; }
		public string? SurName { get; set; }
		public void Mapping(Profile profile)
		{
			profile.CreateMap<Domain.Model.BookAuthor, AuthorVm>()
				.ForMember(a => a.Id, opt => opt.MapFrom(au => au.Author.Id))
				.ForMember(a => a.Name, opt => opt.MapFrom(au => au.Author.Name))
				.ForMember(a => a.SurName, opt => opt.MapFrom(au => au.Author.SurName))
				.ReverseMap();
			profile.CreateMap<Domain.Model.Author, AuthorVm>()
				.ReverseMap();
		}
	}
}

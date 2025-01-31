using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using MyLibraryMVC.Application.Mapping;

namespace MyLibraryMVC.Application.ViewModels.Author
{
	public class NewAuthorVm : IMapFrom<Domain.Model.Author>
	{
		public int? Id { get; set; }
		public string? Name { get; set; }
		public string? SurName { get; set; }
		public string? RealName { get; set; }
		public string? RealSurName { get; set; }
		public DateOnly? DateOfBirth { get; set; }
		//public DateOnly? DateOfDeath { get; set; }
		//public string PlaceOfBirth { get; set; }
		//public string Nationality { get; set; }
		//public string Language { get; set; }

		public void Mapping(Profile profile)
		{
			profile.CreateMap<NewAuthorVm, Domain.Model.Author>()
				.ReverseMap();
		}
	}
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using MyLibraryMVC.Application.Mapping;

namespace MyLibraryMVC.Application.ViewModels.AgeGroup
{
	public class AgeGroupForListVm : IMapFrom<Domain.Model.AgeGroup>
	{
		public int Id { get; set; }
		public string Name { get; set; }
		public void Mapping(Profile profile)
		{
			profile.CreateMap<Domain.Model.AgeGroup, AgeGroupForListVm>();
		}
	}
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using MyLibraryMVC.Application.Mapping;

namespace MyLibraryMVC.Application.ViewModels.House
{
	public class HouseForListVm : IMapFrom<Domain.Model.House>
	{
		public int Id { get; set; }
		public string Name { get; set; }
		public void Mapping(Profile profile)
		{
			profile.CreateMap<Domain.Model.House, HouseForListVm>();
		}
	}
}

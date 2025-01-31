using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using MyLibraryMVC.Application.Mapping;
using MyLibraryMVC.Domain.Model;

namespace MyLibraryMVC.Application.ViewModels.Info
{
	public class NewInfoVm : IMapFrom<Domain.Model.Info>
	{
		public int? Id { get; set; }
		public int? YearOfPublication { get; set; }
		public int? PublishingHouseId { get; set; }
		public string? PublishingHouseName { get; set; }
		public int? NumberOfPublishing { get; set; }			
		public DateOnly? PublishingDate { get; set; }
		public int? CityOfPublishingId { get; set; }
		public string? CityOfPublishingName { get; set; }

		public void Mapping(Profile profile)
		{
			profile.CreateMap<NewInfoVm, Domain.Model.Info>()
				.ForPath(a => a.PublishingHouse.Name, b => b.MapFrom(c => c.PublishingHouseName))
				.ForPath(a => a.CityOfPublishing.Name, b => b.MapFrom(c => c.CityOfPublishingName)).
				ReverseMap();
				;	
		}
	}
}

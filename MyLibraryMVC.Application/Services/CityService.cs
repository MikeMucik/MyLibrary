using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc.Rendering;
using MyLibraryMVC.Application.Interfaces;
using MyLibraryMVC.Application.ViewModels.City;
using MyLibraryMVC.Domain.Interfaces;

namespace MyLibraryMVC.Application.Services
{
	public class CityService : ICityService
	{
		private readonly ICityRepo _cityRepo;
		private readonly IMapper _mapper;
		public CityService(
			ICityRepo cityRepo,
			IMapper mapper)
		{
			_cityRepo = cityRepo;
			_mapper = mapper;
		}
		public List<SelectListItem> GetCitiesForSelectList()
		{
			var cities = _cityRepo.GetAllCity();
			var citiesVms = _mapper.Map<List<CityForListVm>>(cities);
			return citiesVms.Select(c => new SelectListItem
			{
				Value = c.Id.ToString(),
				Text = c.Name,
			}).ToList();
		}
	}
}

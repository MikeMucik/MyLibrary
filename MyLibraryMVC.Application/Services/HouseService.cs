using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc.Rendering;
using MyLibraryMVC.Application.Interfaces;
using MyLibraryMVC.Application.ViewModels.House;
using MyLibraryMVC.Domain.Interfaces;
using MyLibraryMVC.Domain.Model;

namespace MyLibraryMVC.Application.Services
{
	public class HouseService : IHouseService
	{
		private readonly IHouseRepo _houseRepo;
		private readonly IMapper _mapper;
		public HouseService(
			IHouseRepo houseRepo,
			IMapper mapper)
		{
			_houseRepo = houseRepo;
			_mapper = mapper;
		}

		public int AddHouse(string houseName)
		{
			var checkHouse = _houseRepo.GetHouseIdByName(houseName);
			if (checkHouse == 0)
			{
				var house = new House
				{
					Name = houseName
				};
				return _houseRepo.AddHouse(house);
			}
			return checkHouse;
		}

		public List<SelectListItem> GetHousesForSelectList()
		{
			var houses = _houseRepo.GetAllHouse();
			var housesVms = _mapper.Map<List<HouseForListVm>>(houses);
			return housesVms.Select(c => new SelectListItem
			{
				Value = c.Id.ToString(),
				Text = c.Name
			}).ToList();
		}
	}
}

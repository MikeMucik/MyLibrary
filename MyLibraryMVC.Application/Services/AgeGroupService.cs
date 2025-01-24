using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc.Rendering;
using MyLibraryMVC.Application.Interfaces;
using MyLibraryMVC.Application.ViewModels.AgeGroup;
using MyLibraryMVC.Domain.Interfaces;

namespace MyLibraryMVC.Application.Services
{
	public class AgeGroupService : IAgeGroupService
	{
		private readonly IAgeGropuRepo _ageGroupRepo;
		private readonly IMapper _mapper;
		public AgeGroupService(IAgeGropuRepo ageGropuRepo,
						IMapper mapper)
		{
			_ageGroupRepo = ageGropuRepo;
			_mapper = mapper;
		}
		public List<SelectListItem> GetAgeGroupForSelecetList()
		{
			var ageGroups = _ageGroupRepo.GetAllAgeGroup();
			var ageGroupsVms = _mapper.Map<List<AgeGroupForListVm>>(ageGroups);
			return ageGroupsVms.Select(c=> new SelectListItem
			{
				Value = c.Id.ToString(),
				Text = c.Name	
			}).ToList();
		}
	}
}

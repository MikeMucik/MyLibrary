﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using MyLibraryMVC.Application.Mapping;

namespace MyLibraryMVC.Application.ViewModels.Category
{
	public class NewCategoryVm : IMapFrom<Domain.Model.Category>
	{
		public int Id { get; set; }
		public string Name { get; set; }
		public void Mapping(Profile profile)
		{
			profile.CreateMap<NewCategoryVm, Domain.Model.Category>().ReverseMap();
		}
	}
}

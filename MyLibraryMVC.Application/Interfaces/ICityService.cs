using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Rendering;
using MyLibraryMVC.Application.ViewModels.City;

namespace MyLibraryMVC.Application.Interfaces
{
	public interface ICityService
	{
		List<SelectListItem> GetCitiesForSelectList();
		int AddCity (string cityName);
	}
}

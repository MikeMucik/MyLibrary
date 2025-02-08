using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Rendering;
using MyLibraryMVC.Domain.Model;

namespace MyLibraryMVC.Application.Interfaces
{
	public interface IHouseService
	{
		List<SelectListItem> GetHousesForSelectList();
		int AddHouse(string house);
	}
}

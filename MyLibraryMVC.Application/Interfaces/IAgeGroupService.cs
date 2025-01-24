using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace MyLibraryMVC.Application.Interfaces
{
	public interface IAgeGroupService
	{
		List<SelectListItem> GetAgeGroupForSelecetList();
	}
}

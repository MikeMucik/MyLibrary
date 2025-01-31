using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Rendering;
using MyLibraryMVC.Application.ViewModels.Author;

namespace MyLibraryMVC.Application.Interfaces
{
	public interface IAuthorService
	{
		int GetOrAddAuthor(NewAuthorVm item);
		List<SelectListItem> GetAuthorsForSelectList();
	}
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MyLibraryMVC.Domain.Model;

namespace MyLibraryMVC.Application.Interfaces
{
	public interface IBookAuthorService
	{
		void AddBookAuthor(BookAuthor bookAuthor);
	}
}

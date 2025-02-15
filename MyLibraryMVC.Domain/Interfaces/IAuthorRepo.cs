﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MyLibraryMVC.Domain.Model;

namespace MyLibraryMVC.Domain.Interfaces
{
	public interface IAuthorRepo
	{		
		IEnumerable<Author> GetAllAuthors();
		int AddAuthor (Author author);
		int GetAuthorIdByName(string? name, string? surName);
		//Author GetAuthorById (int id);
	}
}

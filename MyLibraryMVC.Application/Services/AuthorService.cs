using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc.Rendering;
using MyLibraryMVC.Application.Interfaces;
using MyLibraryMVC.Application.ViewModels.Author;
using MyLibraryMVC.Domain.Interfaces;
using MyLibraryMVC.Domain.Model;

namespace MyLibraryMVC.Application.Services
{
	public class AuthorService : IAuthorService
	{
		private readonly IAuthorRepo _authorRepo;
		private readonly IMapper _mapper;
		public AuthorService(IAuthorRepo authorRepo,
							IMapper mapper)
		{
			_authorRepo = authorRepo;
			_mapper = mapper;
		}
		public int GetOrAddAuthor(NewAuthorVm item)
		{

			if (item.Id == null)
			{
				var itemBase = _mapper.Map<Author>(item);
				var authorId = _authorRepo.AddAuthor(itemBase);
				return authorId;
			}
			if (item.Id > 0)
			{
				return (int)item.Id;
			}
			else
			{
				throw new InvalidDataException("Number of author shouldn't be minus");
			}
		}
		public List<SelectListItem> GetAuthorsForSelectList()
		{
			var authors = _authorRepo.GetAllAuthors();
			var authorsVms = _mapper.Map<List<AuthorForListVm>>(authors);
			return authorsVms.Select(c => new SelectListItem
			{
				Value = c.Id.ToString(),
				Text = c.Name + " " + c.SurName,
			}).ToList();
		}
	}
}

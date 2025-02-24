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
				var authorExist = _authorRepo.GetAuthorIdByName(item.Name, item.SurName);
				if (authorExist != 0)
				{
					return authorExist;
				}
				else
				{
					var itemBase = _mapper.Map<Author>(item);
					var authorId = _authorRepo.AddAuthor(itemBase);
					return authorId;
				}
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
			return authorsVms
				.OrderBy(c => c.SurName)
				.Select(c => new SelectListItem
			{
				Value = c.Id.ToString(),
				Text = c.Name + " " + c.SurName
			})
				
				.ToList();			
		}
		public List<SelectListItem> GetSelectedAuthors(List<NewAuthorVm> bookAuthors)
		{
			var authors = _authorRepo.GetAllAuthors();
			var bookAuthorIds = bookAuthors.Select(a=>a.Id).ToList();			
			var selectListAuthors = authors.Select(c => new SelectListItem
			{
				Value = c.Id.ToString(),
				Text = c.Name + " " + c.SurName,
				Selected = bookAuthorIds.Contains(c.Id)
			}).ToList();
			return selectListAuthors;
		}
		public AuthorsListVm GetAllAuthors(int pageSize, int pageNumber)
		{
			var authors = _authorRepo.GetAllAuthors()
				.Select(x=>_mapper.Map<AuthorForDetailsList>(x))
				.OrderBy(x=>x.SurName)
				.ToList();
			var authorsToShow = authors
				.Skip(pageSize*(pageNumber-1))
				.Take(pageSize)
				.ToList();

			var authorList = new AuthorsListVm
			{
				AuthorList = authorsToShow,
				CurrentPage = pageNumber,
				PageSize= pageSize,
				TotalCount = authors.Count
				
			};
			return authorList;
		}
		public void DeleteAuthor(int id)
		{
			_authorRepo.DeleteAuthor(id);
		}
		public NewAuthorVm GetAuthorById(int id)
		{
			var author = _authorRepo.GetAuthorById(id);
			var authorVm = _mapper.Map<NewAuthorVm>(author);
			return authorVm;
		}
		public void EditAuthor(NewAuthorVm authorVm)
		{
			var author = _mapper.Map<Author>(authorVm);
			_authorRepo.EditAuthor(author);
		}
	}
}

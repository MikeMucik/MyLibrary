using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using MyLibraryMVC.Application.Interfaces;
using MyLibraryMVC.Domain.Interfaces;
using MyLibraryMVC.Domain.Model;

namespace MyLibraryMVC.Application.Services
{
	public class BookAuthorService : IBookAuthorService
	{
		private readonly IBookAuthorRepo _bookAuthorRepo;		
		public BookAuthorService(IBookAuthorRepo bookAuthorRepo)
		{
			_bookAuthorRepo = bookAuthorRepo;			
		}		
		public void AddBookAuthor(BookAuthor bookAuthor)
		{			
			_bookAuthorRepo.AddBookAuthor(bookAuthor);
		}		
	}
}

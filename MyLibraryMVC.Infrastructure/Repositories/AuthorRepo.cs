using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using MyLibraryMVC.Domain.Interfaces;
using MyLibraryMVC.Domain.Model;

namespace MyLibraryMVC.Infrastructure.Repositories
{
	public class AuthorRepo : IAuthorRepo
	{
		private readonly Context _context;
		public AuthorRepo(Context context)
		{
			_context = context;
		}
		public int AddAuthor(Author author)
		{
			_context.Authors.Add(author);
			_context.SaveChanges();
			return author.Id;
		}
		public IEnumerable<Author> GetAllAuthors()
		{
			return _context.Authors.ToList();
		}
		//public Author GetAuthorById(int id)
		//{
		//	return _context.Authors.FirstOrDefault(x=>x.Id == id);
		//}
	}
}

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
		public int GetAuthorIdByName(string? name, string? surName)
		{
			var existingAuthor = _context.Authors
				.FirstOrDefault(a => a.Name.ToLower().Trim() == name.ToLower().Trim() &&
				a.SurName.ToLower().Trim() == surName.ToLower().Trim());
			return existingAuthor?.Id ?? 0;
		}
		public IEnumerable<Author> GetAllAuthors()
		{
			return _context.Authors.ToList();
		}
		public void DeleteAuthor(int id)
		{
			var author = _context.Authors.FirstOrDefault(a => a.Id == id);
			if (author != null) { _context.Authors.Remove(author); }
			_context.SaveChanges();
		}
		public Author GetAuthorById(int id)
		{
			var author = _context.Authors.FirstOrDefault(a=>a.Id==id);
			return author;
		}
		public void EditAuthor(Author author)
		{
			_context.Authors.Attach(author);
			_context.Entry(author).Property(nameof(author.SurName)).IsModified = true;
			_context.Entry(author).Property(nameof(author.Name)).IsModified = true;
			_context.Entry(author).Property(nameof(author.Language)).IsModified = true;
			_context.Entry(author).Property(nameof(author.RealName)).IsModified = true;
			_context.Entry(author).Property(nameof(author.DateOfBirth)).IsModified = true;
			_context.Entry(author).Property(nameof(author.DateOfDeath)).IsModified = true;
			_context.Entry(author).Property(nameof(author.Nationality)).IsModified = true;
			_context.Entry(author).Property(nameof(author.PlaceOfBirth)).IsModified = true;
			_context.Entry(author).Property(nameof(author.RealSurName)).IsModified = true;
			_context.Entry(author).Property(nameof(author.SurName)).IsModified = true;
			
			_context.SaveChanges();
		}
	}
}

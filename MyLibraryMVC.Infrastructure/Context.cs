using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using MyLibraryMVC.Domain.Model;

namespace MyLibraryMVC.Infrastructure
{
	public class Context(DbContextOptions options) : IdentityDbContext(options)
	{
		public DbSet<AgeGroup> AgeGroups { get; set; }
		public DbSet<Author> Authors { get; set; }
		public DbSet<Book> Books { get; set; }
		public DbSet<BookAuthor> BooksAuthor { get; set; }
		public DbSet<BookInfo> BookInfo { get; set; }
		public DbSet<Category> Categories { get; set; }
		public DbSet<City> Cities { get; set; }
		public DbSet<House> PublishingHouses { get; set; }
		public DbSet<Info> PublishingInfo { get; set; }
		public DbSet<Loan> Loans { get; set; }

		protected override void OnModelCreating(ModelBuilder builder)
		{
			base.OnModelCreating(builder);

			builder.Entity<BookAuthor>()
				.HasKey(it => new { it.BookId, it.AuthorId });

			builder.Entity<BookAuthor>()
				.HasOne<Book>(b => b.Book)
				.WithMany(a => a.BookAuthors)
				.HasForeignKey(b => b.BookId);

			builder.Entity<BookAuthor>()
				.HasOne<Author>(b => b.Author)
				.WithMany(a => a.BookAuthors)
				.HasForeignKey(b => b.AuthorId);

			builder.Entity<Author>()
				.Property(a=>a.DateOfBirth)
				.HasColumnType("date");

			builder.Entity<Info>()
				.Property(h=>h.PublishingDate)
				.HasColumnType("date");

			builder.Entity<Loan>()
				.HasOne<ApplicationUser>()
				.WithMany()
				.HasForeignKey(l => l.UserID);
				//.OnDelete(DeleteBehavior.Cascade);	
		}

	}
}

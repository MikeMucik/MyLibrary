using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using MyLibraryMVC.Domain.Model;

namespace MyLibraryMVC.Infrastructure
{
	public class Context: IdentityDbContext<ApplicationUser>
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

		public Context(DbContextOptions<Context> options) : base(options)
		{

		}
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
				.HasConversion(new ValueConverter<DateOnly?, DateTime?>(
					v=>v.HasValue ?v.Value.ToDateTime(TimeOnly.MinValue): null,
					v=>v.HasValue ?DateOnly.FromDateTime(v.Value):null))
				.HasColumnType("date");

			builder.Entity<Author>()
				.Property(a => a.DateOfDeath)
				.HasConversion(new ValueConverter<DateOnly?, DateTime?>(
					v => v.HasValue ? v.Value.ToDateTime(TimeOnly.MinValue) : null,
					v => v.HasValue ? DateOnly.FromDateTime(v.Value) : null))
				.HasColumnType("date");

			builder.Entity<Info>()
				.Property(h=>h.PublishingDate)
				.HasConversion(new ValueConverter<DateOnly?, DateTime?>(
					v => v.HasValue ? v.Value.ToDateTime(TimeOnly.MinValue) : null,
					v => v.HasValue ? DateOnly.FromDateTime(v.Value) : null))
				.HasColumnType("date");

			builder.Entity<Loan>()
				.Property(a=>a.LoanDate)
				.HasConversion(new ValueConverter<DateOnly?, DateTime?>(
					v => v.HasValue ? v.Value.ToDateTime(TimeOnly.MinValue) : null,
					v => v.HasValue ? DateOnly.FromDateTime(v.Value) : null))
				.HasColumnType("date");

			builder.Entity<Loan>()
				.Property(a=>a.ReturnDate)
				.HasConversion(new ValueConverter<DateOnly?, DateTime?>(
					v => v.HasValue ? v.Value.ToDateTime(TimeOnly.MinValue) : null,
					v => v.HasValue ? DateOnly.FromDateTime(v.Value) : null))
				.HasColumnType("date");

			builder.Entity<Loan>()
				.HasOne<ApplicationUser>()
				.WithMany()
				.HasForeignKey(l => l.UserId);
				//.OnDelete(DeleteBehavior.Cascade);	
		}

	}
}

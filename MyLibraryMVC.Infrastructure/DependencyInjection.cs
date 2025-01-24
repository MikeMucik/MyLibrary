using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using MyLibraryMVC.Domain.Interfaces;
using MyLibraryMVC.Infrastructure.Repositories;

namespace MyLibraryMVC.Infrastructure
{
	public static class DependencyInjection
	{
		public static IServiceCollection AddInfrastructure(this IServiceCollection services)
		{
			services.AddTransient<IAgeGropuRepo, AgeGroupRepo>();
			services.AddTransient<IAuthorRepo, AuthorRepo>();
			services.AddTransient<IBookRepo, BookRepo>();
			services.AddTransient<IBookAuthorRepo, BookAuthorRepo>();
			services.AddTransient<IBookInfoRepo, BookInfoRepo>();
			services.AddTransient<ICategoryRepo, CategoryRepo>();
			services.AddTransient<ICityRepo, CityRepo>();
			services.AddTransient<IHouseRepo, HouseRepo>();
			services.AddTransient<IInfoRepo, InfoRepo>();
			services.AddTransient<ILoanRepo, LoanRepo>();
			return services;
		}
	}
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using MyLibraryMVC.Application.Interfaces;
using MyLibraryMVC.Application.Mapping;
using MyLibraryMVC.Application.Services;

namespace MyLibraryMVC.Application
{
	public static class DependecyInjection
	{
		public static IServiceCollection AddAplication(this IServiceCollection services)
		{
			services.AddTransient<IAgeGroupService, AgeGroupService>();
			services.AddTransient<IAuthorService, AuthorService>();
			services.AddTransient<IBookService, BookService>();
			services.AddTransient<IBookInfoService, BookInfoService>();
			services.AddTransient<IBookAuthorService, BookAuthorService>();
			services.AddTransient<ICategoryService, CategoryService>();
			services.AddTransient<ICityService, CityService>();
			services.AddTransient<IHouseService, HouseService>();
			services.AddTransient<IInfoservice, InfoService>();
			services.AddTransient<ILoanService, LoanService>();			
			services.AddAutoMapper(Assembly.GetExecutingAssembly());
			return services;
		}
	}
}

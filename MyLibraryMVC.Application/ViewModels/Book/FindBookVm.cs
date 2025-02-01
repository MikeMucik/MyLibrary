using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using MyLibraryMVC.Application.Mapping;
using MyLibraryMVC.Domain.Model;

namespace MyLibraryMVC.Application.ViewModels.Book
{
	public class FindBookVm : IMapFrom<Domain.Model.Book>
	{
		public List<BookForListVm>? Books { get; set; }
		public int CurrentPage { get; set; }
		public int PageNumber { get; set; }
		public int PageSize { get; set; }
		public int TotalCount { get; set; }
		public int AuthorId { get; set; }
		public int NumberPages { get; set; }
		public int CategoryId { get; set; }
		public int AgeGroupId { get; set; }
		public int YearOfPublishing {  get; set; }
		public int HouseOfPublishingId { get; set; }
		public void Mapping(Profile profile)
		{
			profile.CreateMap<FindBookVm, Domain.Model.Book>();
		}
	}
}

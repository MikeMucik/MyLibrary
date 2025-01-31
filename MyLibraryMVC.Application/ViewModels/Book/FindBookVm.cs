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

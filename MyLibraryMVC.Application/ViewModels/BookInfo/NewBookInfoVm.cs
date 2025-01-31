using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using MyLibraryMVC.Application.Mapping;
using MyLibraryMVC.Domain.Model;

namespace MyLibraryMVC.Application.ViewModels.BookInfo
{
	public class NewBookInfoVm : IMapFrom<Domain.Model.BookInfo>
	{
		public int? Id { get; set; }
		public int? NumberOfPages { get; set; }
		public int? NumberOfChapter { get; set; }
		public bool? Illustration { get; set; }
		public string? Binding { get; set; }
		public string? Subtitle { get; set; }				
		public int? AgeGroupId { get; set; }		
		public void Mapping(Profile profile)
		{
			profile.CreateMap<NewBookInfoVm, Domain.Model.BookInfo>();
		}
	}
}

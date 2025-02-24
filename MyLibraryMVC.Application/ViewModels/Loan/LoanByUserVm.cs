using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using MyLibraryMVC.Application.Mapping;

namespace MyLibraryMVC.Application.ViewModels.Loan
{
	public class LoanByUserVm :IMapFrom<Domain.Model.Loan>
	{
		public int Id { get; set; }
		public int BookId { get; set; }
		public string BookTitle { get; set; }
		public DateOnly LoanDate { get; set; }
		public DateOnly ReturnDate { get; set; }
		//public bool IsReturned { get; set; }
		public void Mapping(Profile profile)
		{
			profile.CreateMap<Domain.Model.Loan, LoanByUserVm>();
		}
	}
}

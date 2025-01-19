using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using MyLibraryMVC.Domain.Model;

namespace MyLibraryMVC.Infrastructure
{
	public class ApplicationUser : IdentityUser
	{
		public required string Name { get; set; }
		public required string SurName { get; set; }
	}
}

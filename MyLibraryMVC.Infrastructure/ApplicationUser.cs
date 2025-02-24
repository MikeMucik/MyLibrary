﻿using System;
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
		public string? FirstName { get; set; }
		public string? SurName { get; set; }
	}
}

﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyLibraryMVC.Domain.Model
{
	public class Category
	{
		public int Id { get; set; }
		public required string Name { get; set; }
	}
}

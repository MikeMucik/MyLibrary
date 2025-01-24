using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MyLibraryMVC.Domain.Interfaces;
using MyLibraryMVC.Domain.Model;

namespace MyLibraryMVC.Infrastructure.Repositories
{
	public class AgeGroupRepo : IAgeGropuRepo
	{
		private readonly Context _context;
		public AgeGroupRepo(Context context)
		{
			_context = context;
		}
		public IEnumerable<AgeGroup> GetAllAgeGroup()
		{
			return _context.AgeGroups.ToList();
		}
	}
}

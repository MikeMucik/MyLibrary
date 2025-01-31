using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MyLibraryMVC.Domain.Interfaces;
using MyLibraryMVC.Domain.Model;

namespace MyLibraryMVC.Infrastructure.Repositories
{
	public class CityRepo : ICityRepo
	{
		private readonly Context _context;
		public CityRepo(Context context)
		{
			_context = context;
		}
		public IEnumerable<City> GetAllCity()
		{
			return _context.Cities.ToList();
		}
	}
}

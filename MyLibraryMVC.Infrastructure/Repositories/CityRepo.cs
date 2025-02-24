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

		public int AddCity(City city)
		{
			_context.Cities.Add(city);
			_context.SaveChanges();
			return city.Id;
		}
		public IEnumerable<City> GetAllCity()
		{
			return _context.Cities
				.OrderBy(c => c.Name)
				.ToList();
		}
		public int GetCityIdByName(string cityName)
		{
			var existingCity = _context.Cities
				.FirstOrDefault(c => c.Name.ToLower().Trim() == cityName.ToLower().Trim());
			return existingCity?.Id ?? 0;
		}
	}
}

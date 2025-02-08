using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MyLibraryMVC.Domain.Interfaces;
using MyLibraryMVC.Domain.Model;

namespace MyLibraryMVC.Infrastructure.Repositories
{
	public class HouseRepo : IHouseRepo
	{
		private readonly Context _context;
		public HouseRepo(Context context)
		{
			_context = context;
		}
		public int AddHouse(House house)
		{
			_context.PublishingHouses.Add(house);
			_context.SaveChanges();
			return house.Id;
		}
		public int GetHouseIdByName(string houseName)
		{
			var existingHouse = _context.PublishingHouses
				.FirstOrDefault(h => h.Name.ToLower().Trim() == houseName.ToLower().Trim());
			return existingHouse?.Id ?? 0;
		}
		public IEnumerable<House> GetAllHouse()
		{
			return _context.PublishingHouses.ToList();
		}
	}
}
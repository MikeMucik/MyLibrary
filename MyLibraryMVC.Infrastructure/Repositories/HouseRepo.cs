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
		public IEnumerable<House> GetAllHouse()
		{
			return _context.PublishingHouses.ToList();
		}
	}
}

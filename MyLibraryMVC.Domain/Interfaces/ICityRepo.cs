using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MyLibraryMVC.Domain.Model;

namespace MyLibraryMVC.Domain.Interfaces
{
	public interface ICityRepo
	{
		int AddCity(City city);
		IEnumerable<City> GetAllCity();
		int GetCityIdByName(string cityName);
	}
}

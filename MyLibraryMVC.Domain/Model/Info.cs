using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyLibraryMVC.Domain.Model
{
	public class Info
	{
		public int Id { get; set; }
		public int? YearOfPublication { get; set; }
		public int? PublishingHouseId { get; set; }
		public virtual House? PublishingHouse { get; set; }
		public int? NumberOfPublishing { get; set; }
		public DateOnly? PublishingDate { get; set; }
		public int? CityOfPublishingId { get; set; }
		public virtual City? CityOfPublishing { get; set; }
	}
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyLibraryMVC.Application.ViewModels.ManagerUser
{
	public class ManageUserVm
	{
		public string UserId { get; set; }
		public string UserName { get; set; }
		public bool IsUser { get; set; }		
		public bool IsAdmin { get; set; }
	}
}

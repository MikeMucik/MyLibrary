using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyLibraryMVC.Application.ViewModels.ManagerUser
{
	public class ListManageUsersVm
	{
		public List<ManageUserVm> ListUsers {  get; set; } = new List<ManageUserVm>();
	}
}

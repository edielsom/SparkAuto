using System.Collections.Generic;

namespace SparkAuto.Models.ViewModel
{
    public class UsersListViewModel
    {
        public List<ApplicationUser> ApplicationUserList { get; set; }
        public PageingInfo PageingInfo { get; set; }
    }
}

using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using SparkAuto.Areas.Utily;
using SparkAuto.Data;
using SparkAuto.Models;
using SparkAuto.Models.ViewModel;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SparkAuto.Pages.Users
{
    public class IndexModel : PageModel
    {
        private readonly ApplicationDbContext _db;

        public IndexModel(ApplicationDbContext db)
        {
            this._db = db;
        }

        [BindProperty]
        public UsersListViewModel UsersListVM { get; set; }

        public async Task<IActionResult> OnGet(int productPage = 1,string searchEmail = null,string searchName = null,string searchPhone = null)
        {
            UsersListVM = new UsersListViewModel()
            {
                ApplicationUserList = await _db.ApplicationUser.ToListAsync()
            };

            StringBuilder param = new StringBuilder();
            param.Append("/Users?productPage=:");

           
            if (!string.IsNullOrEmpty(searchEmail))
            {
                param.Append("&searchEmail=");
                param.Append(searchEmail);
                UsersListVM.ApplicationUserList = await _db.ApplicationUser.Where(u => u.Email.Contains(searchEmail)).ToListAsync();
            }
            else
            {
                if (!string.IsNullOrEmpty(searchName))
                {
                    param.Append("&searchName=");
                    param.Append(searchName);
                    UsersListVM.ApplicationUserList = await _db.ApplicationUser.Where(u => u.Name.Contains(searchName)).ToListAsync();
                }
                else
                {
                    if (!string.IsNullOrEmpty(searchPhone))
                    {
                        param.Append("&searchPhone=");
                        param.Append(searchPhone);
                        UsersListVM.ApplicationUserList = await _db.ApplicationUser.Where(u => u.PhoneNumber.Contains(searchPhone)).ToListAsync();
                    }
                }
            }

            var count = UsersListVM.ApplicationUserList.Count;

            UsersListVM.PageingInfo = new PageingInfo
            {
                CurrentPage = productPage,
                ItemsPerPage = SD.PaginationUsersPage,
                TotalItems = count,
                UrlParam = param.ToString()
            };

            UsersListVM.ApplicationUserList = UsersListVM.ApplicationUserList.OrderBy(p => p.Email)
                .Skip((productPage - 1) * SD.PaginationUsersPage)
                .Take(SD.PaginationUsersPage).ToList();

            return Page();
        }
    }
}
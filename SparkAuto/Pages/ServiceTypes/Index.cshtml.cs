using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using SparkAuto.Areas.Utily;
using SparkAuto.Data;
using SparkAuto.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SparkAuto.Pages.ServiceTypes
{
    [Authorize(Roles = SD.AdminEndUser)]
    public class IndexModel : PageModel
    {
        private readonly ApplicationDbContext _db;
        public IList<ServiceType> ServiceType;
        public IndexModel(ApplicationDbContext db)
        {
            this._db = db;
            this.ServiceType = new List<ServiceType>();
        }

        public async Task<ActionResult> OnGet()
        {
            ServiceType = await this._db.ServiceTypes.ToListAsync();
            return Page();
        }
    }
}
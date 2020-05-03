using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using SparkAuto.Data;
using SparkAuto.Models;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SparkAuto.Pages.ServiceTypes
{
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
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using SparkAuto.Areas.Utily;
using SparkAuto.Data;
using SparkAuto.Models;
using System.Linq;
using System.Threading.Tasks;

namespace SparkAuto.Pages.ServiceTypes
{
    [Authorize(Roles = SD.AdminEndUser)]
    public class EditModel : PageModel
    {
        private readonly ApplicationDbContext _db;

        public EditModel(ApplicationDbContext db)
        {
            _db = db;
        }

        [BindProperty]
        public ServiceType ServiceType { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            ServiceType = await _db.ServiceTypes.FirstOrDefaultAsync(m => m.Id == id);

            if (ServiceType == null)
            {
                return NotFound();
            }
            return Page();
        }
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            var serviceFromDb = await _db.ServiceTypes.FirstOrDefaultAsync(s => s.Id == ServiceType.Id);
            serviceFromDb.Name = ServiceType.Name;
            serviceFromDb.Price = ServiceType.Price;


            _db.ServiceTypes.Update(serviceFromDb);

            await _db.SaveChangesAsync();

            return RedirectToPage("./Index");
        }

        private bool ServiceTypeExists(int id)
        {
            return _db.ServiceTypes.Any(e => e.Id == id);
        }
    }
}

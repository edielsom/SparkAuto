using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.EntityFrameworkCore;
using SparkAuto.Areas.Utily;
using SparkAuto.Data;
using SparkAuto.Models;
using System.Threading.Tasks;

namespace SparkAuto.Pages.Users
{
    [Authorize(Roles = SD.AdminEndUser)]
    public class DeleteModel : PageModel
    {
        private readonly ApplicationDbContext _db;
        public DeleteModel(ApplicationDbContext db)
        {
            this._db = db;
        }

        [BindProperty]
        public ApplicationUser  ApplicationUser { get; set; }

        public async Task<IActionResult> OnGetAsync(string id)
        {
            if (string.IsNullOrEmpty(id))
                return NotFound();

            ApplicationUser = await _db.ApplicationUser.FirstOrDefaultAsync(u => u.Id == id);

            if (ApplicationUser is null)
                return NotFound();

            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            var userInDb = await _db.Users.SingleOrDefaultAsync(u => u.Id == ApplicationUser.Id);

            _db.Users.Remove(userInDb);
            await _db.SaveChangesAsync();

            return RedirectToPage("Index");
        }
    }
}
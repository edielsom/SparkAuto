using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using SparkAuto.Data;
using SparkAuto.Models;
using System.Threading.Tasks;

namespace SparkAuto.Pages.ServiceTypes
{
    public class CreateModel : PageModel
    {
        private readonly SparkAuto.Data.ApplicationDbContext _db;

        [BindProperty]
        public ServiceType ServiceType { get; set; }

        public CreateModel(ApplicationDbContext db)
        {
            this._db = db;
        }

        public IActionResult OnGet()
        {
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            //Se o Mode for inválido, retorna para página de criação.
            if (!ModelState.IsValid)
                return Page();

            this._db.ServiceTypes.Add(ServiceType);

            await this._db.SaveChangesAsync();
            return RedirectToPage("Index");
        }
    }
}
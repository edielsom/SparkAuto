using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace SparkAuto.Areas.Identity.Pages
{
    public class VerfiyEmailModel : PageModel
    {
        [BindProperty]
        public string Email { get; set; }
        public IActionResult OnGet(string id)
        {
            Email = id;
            return Page();
        }
    }
}

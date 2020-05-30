using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using SparkAuto.Data;
using SparkAuto.Models.ViewModel;

namespace SparkAuto.Pages.Cars
{
    public class IndexModel : PageModel
    {
        private readonly ApplicationDbContext _db;

        [BindProperty]
        public CarAndCustomerViewModel CarAndCustVM { get; set; }
        [TempData]
        public string StatusMessage { get; set; }
        public IndexModel(ApplicationDbContext db)
        {
            this._db = db;
        }
        public async Task<IActionResult> OnGet(string userId = null)
        {
            //Verifica se o valor do usuário é nulo, 
            // então faz a pesquisa para obter as informações do usuário através do acesso do usuário.
            if (string.IsNullOrEmpty(userId))
            {
                var claimsIdentity = (ClaimsIdentity)User.Identity;
                var claim = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier);
                userId = claim.Value;
            }

            //Inicializa e Preenche as informações da ViewModel de Carros e Clientes
            // Observação : Os Clientes são os usuários cadastros com estatos de Clientes.

            CarAndCustVM = new CarAndCustomerViewModel()
            {
                Cars = await _db.Cars.Where(c => c.UserId == userId).ToListAsync(),
                UserObj = await _db.ApplicationUser.FirstOrDefaultAsync(u => u.Id == userId)
            };
            return Page();
        }
    }
}
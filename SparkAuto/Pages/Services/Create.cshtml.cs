using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ApplicationModels;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using SparkAuto.Data;
using SparkAuto.Models;
using SparkAuto.Models.ViewModel;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Threading.Tasks;

namespace SparkAuto.Pages.Services
{
    [Authorize]
    public class CreateModel : PageModel
    {
        private readonly ApplicationDbContext _db;

        [BindProperty]
        public CarServiceViewModel CarServiceVM { get; set; }
        public CreateModel(ApplicationDbContext db)
        {
            _db = db;
        }

        public async Task<IActionResult> OnGet(int carId)
        {
            CarServiceVM = new CarServiceViewModel
            {
                Car = await _db.Cars.Include(c => c.ApplicationUser)
                                    .FirstOrDefaultAsync(c => c.Id == c.Id),
                ServiceHeader = new Models.ServiceHeader()
            };

            List<string> lstServiceTypeInShoppingCard = _db.ServiceShoppingCars
                                                        .Include(c => c.ServiceType)
                                                        .Where(c => c.CarId == carId)
                                                        .Select(c => c.ServiceType.Name)
                                                        .ToList();

            IQueryable<ServiceType> lstService = from s in _db.ServiceTypes
                                                 where !(lstServiceTypeInShoppingCard.Contains(s.Name))
                                                 select s;

            CarServiceVM.ServiceTypesList = lstService.ToList();

            CarServiceVM.ServiceShoppingCarsList = _db.ServiceShoppingCars
                                                    .Include(c => c.ServiceType)
                                                    .Where(c => c.CarId == carId)
                                                    .ToList();
            CarServiceVM.ServiceHeader.TotalPrice = 0;

            //Retorna o total dos serviços contratados.
            foreach (var item in CarServiceVM.ServiceShoppingCarsList)
            {
                CarServiceVM.ServiceHeader.TotalPrice += item.ServiceType.Price;
            }

            return Page();
        }
    }
}
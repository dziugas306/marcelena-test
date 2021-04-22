using Marcelena_web.Domain.Entitites;
using Marcelena_web.Services.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Marcelena_web.Pages
{
    public class HomeModel : PageModel
    {
        [BindProperty]
        public List<Shop> Shop { get; set; }
        private readonly IShopRepository shopRepository;
        public HomeModel(IShopRepository shopRepository)
        {
            this.shopRepository = shopRepository;
        }
        public async Task<IActionResult> OnGetAsync()
        {
            Shop = new List<Shop>(await shopRepository.GetAllShopsAsync());
            Shop.Reverse();

            return Page();
        }
    }
}

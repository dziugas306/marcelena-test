using Marcelena_web.Domain.Entitites;
using Marcelena_web.Services;
using Marcelena_web.Services.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using System;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;

namespace Marcelena_web.Pages.Edit
{
    public class ProductsModel : PageModel
    {
        private readonly IShopRepository shopRepository;
        [BindProperty]
        public Shop Shop { get; set; }

        public ProductsModel(IShopRepository shopRepository)
        {
            this.shopRepository = shopRepository;
        }

        public async Task<IActionResult> OnGetAsync(int id)
        {
            Shop = await shopRepository.GetShopAsync(id);
            Shop.Coordinate = LocationService.GetCoordinates(Shop.ShopAddress);
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (ModelState.IsValid)
            {
                var shop = await shopRepository.UpdateShopAsync(Shop);
                return Redirect("~/Details/" + shop._id);
            }
            else
            {
                return Redirect("/Edit/Products/"+ Shop._id);
            }
        }
        public ActionResult OnGetProductsPartial(Int32 Index)
        {
            return new PartialViewResult()
            {
                ViewName = "partialProduct",
                ViewData = new ViewDataDictionary<Int32>(ViewData, Index)
            };
        }
    }
}

using Marcelena_web.Domain.Entitites;
using Marcelena_web.Services;
using Marcelena_web.Services.Repositories;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.IO;
using System.Threading.Tasks;

namespace Marcelena_web.Pages
{
    public class EditModel : PageModel
    {
        private readonly IShopRepository shopRepository;
        [BindProperty]
        public Shop Shop { get; set; }

        public EditModel(IShopRepository _shopRepository)
        {
            shopRepository = _shopRepository;
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
                Shop.Coordinate = LocationService.GetCoordinates(Shop.ShopAddress);
                var shop = await shopRepository.UpdateShopAsync(Shop);
                return Redirect("~/Details/" + shop._id);
            }
            else
            {
                return Page();
            }
        }
    }
}

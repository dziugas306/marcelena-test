using Marcelena_web.Domain.Entitites;
using Marcelena_web.Services;
using Marcelena_web.Services.Repositories;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using System;
using System.Collections.Generic;

namespace Marcelena_web.Pages
{
    public class AddPlaceModel : PageModel
    {
        private readonly IShopRepository shopRepository;
        private readonly IWebHostEnvironment webHostEnvironment;
        [BindProperty]
        public Shop Shop { get; set; }
        [BindProperty]
        public List<IFormFile> Photo { get; set; }

        public AddPlaceModel(IShopRepository shopRepository, IWebHostEnvironment _webHostEnvironment)
        {
            this.shopRepository = shopRepository;
            webHostEnvironment = _webHostEnvironment;
        }

        public void OnGet()
        {

        }

        public IActionResult OnPost()
        {
            if (ModelState.IsValid)
            {
                Shop.Reviews = new List<Review>();
                Shop.PhotoPaths = new List<Photo>();
                if (Photo.Count > 0)
                {
                    Shop.PhotoPaths = FileService.GetPhotoPaths(Photo, webHostEnvironment.WebRootPath);
                }
                Shop.Coordinate = LocationService.GetCoordinates(Shop.ShopAddress);
                shopRepository.AddShop(Shop);
                return RedirectToPage("./Search/Index");
            }
            else
            {
                return Page();
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


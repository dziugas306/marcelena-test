using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Marcelena_web.Domain.Entitites;
using Marcelena_web.Services;
using Marcelena_web.Services.Repositories;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Marcelena_web.Pages.Edit
{
    public class PhotosModel : PageModel
    {
        private readonly IShopRepository shopRepository;
        private readonly IWebHostEnvironment _webHostEnvironment;
        [BindProperty]
        public Shop Shop { get; set; }
        [BindProperty]
        [DataType(DataType.Upload)]
        public List<IFormFile> Photo { get; set; }

        public PhotosModel(IShopRepository shopRepository, IWebHostEnvironment webHostEnvironment)
        {
            this.shopRepository = shopRepository;
            _webHostEnvironment = webHostEnvironment;
        }

        public async Task<IActionResult> OnGetAsync(int id)
        {
            Shop = await shopRepository.GetShopAsync(id);
            Shop.Coordinate = LocationService.GetCoordinates(Shop.ShopAddress);
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (Shop.PhotoPaths == null)
            {
                Shop.PhotoPaths = new List<Photo>();
            }
            if (ModelState.IsValid)
            {
                if (Photo.Count > 0)
                {
                    Shop.PhotoPaths.AddRange(FileService.GetPhotoPaths(Photo, _webHostEnvironment.WebRootPath));
                }
                Shop.Coordinate = LocationService.GetCoordinates(Shop.ShopAddress);
                var shop = await shopRepository.UpdateShopAsync(Shop);
                return Redirect("~/Details/" + shop._id);
            }
            else
            {
                return Page();
            }
        }
        public IActionResult OnPostRemove(int id)
        {
            FileService.DeletePhotoFile(Shop.PhotoPaths[id], _webHostEnvironment.WebRootPath);
            shopRepository.DeletePhotoPath(Shop.PhotoPaths[id]._id);
            return Redirect("/Edit/Photos/" + Shop._id);
        }
    }
}

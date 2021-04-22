using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Marcelena_web.Domain.Entitites;
using Marcelena_web.Services;
using Marcelena_web.Services.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Marcelena_web.Pages
{
    public class ReviewModel : PageModel
    {
        private readonly IShopRepository shopRepository;
        private readonly UserManager<User> _userManager;
        private readonly IUserRepository _userRepository;
        [BindProperty]
        public Review Review { get; set; }
        [BindProperty]
        public Shop Shop { get; set; }
        public User currentUser { get; set; }

        public ReviewModel(IShopRepository _shopRepository, UserManager<User> userManager, IUserRepository userRepository)
        {
            this.shopRepository = _shopRepository;
            _userManager = userManager;
            _userRepository = userRepository;
        }

        public async Task<IActionResult> OnGetAsync(int id)
        {
            currentUser = await _userManager.GetUserAsync(User);
            Shop = await shopRepository.GetShopAsync(id);
            if (currentUser != null)
            {
                currentUser = await _userRepository.GetUserAsync(currentUser.Id);
                if (currentUser.UserAddress != null)
                {
                    currentUser.Coordinate = LocationService.GetCoordinates(currentUser.UserAddress);
                }
            }
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int id)
        {
            currentUser = await _userManager.GetUserAsync(User);
            if (currentUser != null)
            {
                currentUser = await _userRepository.GetUserAsync(currentUser.Id);
            }
            if (Shop.Reviews == null)
            {
                Shop.Reviews = new List<Review>();
            }
            Review.userId = currentUser.Id;
            if (ModelState.IsValid)
            {
                Review.Time = DateTime.Now;
                Shop = await shopRepository.GetShopAsync(id);
                Shop.Reviews.Add(Review);
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

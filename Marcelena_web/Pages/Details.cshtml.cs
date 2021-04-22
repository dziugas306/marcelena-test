using Marcelena_web.Domain.Entitites;
using Marcelena_web.Services;
using Marcelena_web.Services.Repositories;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Marcelena_web.Pages
{
    [IgnoreAntiforgeryToken(Order = 1001)]
    public class DetailsModel : PageModel
    {
        private readonly UserManager<User> _userManager;
        private readonly IUserRepository _userRepository;
        private readonly IShopRepository shopRepository;
        public Shop Shop { get; private set; }
        public string ApiUrl { get; private set; }
        public bool EnableDistance { get; set; }
        public bool IsFavorite { get; set; }
        public User currentUser { get; set; }
        public double Distance { get; private set; }
        public List<string> userNames { get; private set; }

        public DetailsModel(IShopRepository shopRepository, IUserRepository userRepository, UserManager<User> userManager)
        {
            this.shopRepository = shopRepository;
            _userManager = userManager;
            _userRepository = userRepository;
        }

        public async Task<IActionResult> OnGetAsync(int id)
        {
            currentUser = await _userManager.GetUserAsync(User);
            Shop = await shopRepository.GetShopAsync(id);
            userNames = new List<string>();
            for (int i = 0; i < Shop.Reviews.Count; i++)
            {
                var test = await _userManager.FindByIdAsync(Shop.Reviews[i].userId);
                userNames.Add(test.UserName);
            }
            if (currentUser != null)
            {
                currentUser = await _userRepository.GetUserAsync(currentUser.Id);
                IsFavorite = currentUser.isShopFavorite(Shop, currentUser.Favorites);
                if (currentUser.UserAddress != null)
                {
                    EnableDistance = true;
                    Distance = Math.Round(LocationService.CalculateDistance(Shop.Coordinate, currentUser.Coordinate) / 1000, 2);
                }
            }
            ApiUrl = LocationService.GetMapsUrl(Shop.ShopAddress.ToString());
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int shopId)
        {
            Shop = await shopRepository.GetShopAsync(shopId);
            currentUser = await _userManager.GetUserAsync(User);
            currentUser = await _userRepository.GetUserAsync(currentUser.Id);
            if (currentUser.isShopFavorite(Shop, currentUser.Favorites))
            {
                Shop.Users.Remove(currentUser);
                currentUser.Favorites.Remove(Shop);
                
            }
            else
            {
                Shop.Users.Add(currentUser);
                currentUser.Favorites.Add(Shop);
            }
            Shop = await shopRepository.UpdateShopAsync(Shop);
            currentUser = _userRepository.UpdateUser(currentUser);
            return new EmptyResult();
        }

        public IActionResult OnPostRemove(int shopId)
        {
            shopRepository.DeleteShop(shopId);
            return RedirectToPage("Search/Index");
        }

        public IActionResult OnPostDelete(int id, int reviewId)
        {
            shopRepository.DeleteReview(reviewId);
            return Redirect("/Details/" + id);
        }
        public string getRole()
        {
            if (currentUser.Role != null)
            {
                return currentUser.Role;
            }
            else
            {
                return null;
            }

        }
    }
}

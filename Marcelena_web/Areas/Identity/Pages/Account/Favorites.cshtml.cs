using Marcelena_web.Domain.Entitites;
using Marcelena_web.Services;
using Marcelena_web.Services.Repositories;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Marcelena_web.Areas.Identity.Pages.Account
{
    public class FavoritesModel : PageModel
    {
        private readonly UserManager<User> _userManager;
        private readonly IUserRepository _userRepository;
        [BindProperty(SupportsGet = true)]
        public string Query { get; set; }
        [BindProperty]
        public List<Shop> Favorites { get; set; }
        public User currentUser { get; set; }

        public FavoritesModel(IUserRepository userRepository, UserManager<User> userManager)
        {
            _userManager = userManager;
            _userRepository = userRepository;
        }

        public async Task<IActionResult> OnGetAsync()
        {
            currentUser = await _userManager.GetUserAsync(User);
            currentUser = await _userRepository.GetUserAsync(currentUser.Id);
            Favorites = new List<Shop>(await _userRepository.GetAllFavoritesAsync(currentUser));
            if (!string.IsNullOrEmpty(Query))
            {
                Favorites = ShopService.ShopByQuery(Favorites, Query);
            }
            return Page();
        }
    }
}

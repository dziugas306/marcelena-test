using Marcelena_web.Domain.Entitites;
using Marcelena_web.Services;
using Marcelena_web.Services.Repositories;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Threading.Tasks;

namespace Marcelena_web.Areas.Identity.Pages.Account
{
    public class UserAddressModel : PageModel
    {
        private readonly UserManager<User> _userManager;
        private readonly IUserRepository _userRepository;
        [BindProperty]
        public User CurrentUser { get; set; }

        public UserAddressModel(IUserRepository userRepository, UserManager<User> userManager)
        {
            _userManager = userManager;
            _userRepository = userRepository;
        }

        public async Task OnGetAsync(int id)
        {
            CurrentUser = await _userManager.GetUserAsync(User);

            if (CurrentUser != null)
            {
                CurrentUser = await _userRepository.GetUserAsync(CurrentUser.Id);
                if (CurrentUser.UserAddress != null)
                {
                    CurrentUser.Coordinate = LocationService.GetCoordinates(CurrentUser.UserAddress);
                }
            }
        }
        public IActionResult OnPost()
        {
            if (ModelState.IsValid)
            {
                CurrentUser.Coordinate = LocationService.GetCoordinates(CurrentUser.UserAddress);
                var user = _userRepository.UpdateUser(CurrentUser);
                return RedirectToPage("./Profile");
            }
            else
            {
                return RedirectToPage("./Profile");
            }
        }
    }
}

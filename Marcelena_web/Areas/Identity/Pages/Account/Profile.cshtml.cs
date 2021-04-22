using System.Threading.Tasks;
using Marcelena_web.Domain.Entitites;
using Marcelena_web.Services.Repositories;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;
using Marcelena_web.Data;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace Marcelena_web.Areas.Identity.Pages.Account
{
    public class ProfileModel : PageModel
    {
        private readonly UserManager<User> _userManager;
        private readonly IUserRepository _userRepository;
        public User currentUser = new User();

        public ProfileModel(IUserRepository userRepository, UserManager<User> userManager)
        {
            _userManager = userManager;
            _userRepository = userRepository;
        }

        public async Task OnGetAsync()
        {
            currentUser = await _userManager.GetUserAsync(User);
            currentUser = await _userRepository.GetUserAsync(currentUser.Id);
        }
    }
}

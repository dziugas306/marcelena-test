using Marcelena_web.Domain.Entitites;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Marcelena_web
{
    [Route("user")]
    public class AccountController : Controller
    {
        private UserManager<User> _userManager;

        //class constructor
        public AccountController(UserManager<User> userManager)
        {
            _userManager = userManager;
        }

        //public Task<User> GetCurrentUserAsync()
        //{
        //    return _userManager.GetUserAsync(HttpContext.User);
        //}
    }
}

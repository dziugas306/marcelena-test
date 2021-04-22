using Marcelena_web.Domain.Entitites;
using Marcelena_web.Services;
using Marcelena_web.Services.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Threading.Tasks;
using System;
using System.Linq;

namespace Marcelena_web.Pages.Admin
{
    public class AdminModel : PageModel
    {
        private readonly UserManager<User> _userManager;
        private readonly IUserRepository _userRepository;
        private readonly IShopRepository _shopRepository;
        [BindProperty(SupportsGet = true)]
        public string Query { get; set; }
        [BindProperty]
        public List<Shop> Shop { get; set; }
        public List<User> Users { get; set; }
        public User currentUser { get; set; }
        public Coordinate userCoordinate { get; set; }

        public AdminModel(IShopRepository shopRepository, IUserRepository userRepository, UserManager<User> userManager)
        {
            _shopRepository = shopRepository;
            _userRepository = userRepository;
            _userManager = userManager;
        }

        public async Task<IActionResult> OnGetAsync()
        {
            Shop = new List<Shop>(await _shopRepository.GetAllShopsAsync());
            if (!string.IsNullOrEmpty(Query))
            {
                Shop = ShopService.ShopByQuery(Shop, Query);
            }
            Users = new List<User>(await _userRepository.GetAllUserAsync());
            return Page();
        }

        public async Task<IActionResult> OnGetOrderPartial(int orderby, int filterby, int filterEQ, string number, string query)
        {
            Shop = new List<Shop>(await _shopRepository.GetAllShopsAsync());
            Users = new List<User>(await _userRepository.GetAllUserAsync());
            if (!string.IsNullOrEmpty(query))
            {
                Shop = ShopService.ShopByQuery(Shop, query);
            }
            currentUser = await _userManager.GetUserAsync(User);
            if (currentUser != null)
            {
                currentUser = await _userRepository.GetUserAsync(currentUser.Id);
                userCoordinate = currentUser.Coordinate;
            }
            Shop = ShopService.DoSort(Shop, orderby, query, userCoordinate);
            Shop = ShopService.DoFilter(Shop, filterby, filterEQ, number, query, userCoordinate);
            var converted = JsonConvert.SerializeObject(Shop);
            Shop = new List<Shop>(await _shopRepository.GetAllShopsAsync());
            return new JsonResult(converted);
        }

        public async Task<IActionResult> OnPostAsync(int id)
        {
            Users = new List<User>(await _userRepository.GetAllUserAsync());
            _shopRepository.DeleteShop(id);
            Shop = new List<Shop>(await _shopRepository.GetAllShopsAsync());
            return Page();
        }
        public async Task<IActionResult> OnPostEditAsync(string id)
        {

            Users = new List<User>(await _userRepository.GetAllUserAsync());
            var a = Users.FirstOrDefault(x => x.UserName == id);
            _userRepository.DeleteUserAddressAndCoordinate(a);
            Shop = new List<Shop>(await _shopRepository.GetAllShopsAsync());
            return RedirectToPage("UserList"); 
        }
    }
}
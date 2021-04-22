using Marcelena_web.Domain.Entitites;
using Marcelena_web.Services;
using Marcelena_web.Services.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Marcelena_web.Pages.Search
{
    public class IndexModel : PageModel
    {
        private readonly UserManager<User> _userManager;
        private readonly IUserRepository _userRepository;
        private readonly IShopRepository _shopRepository;
        [BindProperty(SupportsGet = true)]
        public string Query { get; set; }
        [BindProperty]
        public List<Shop> Shop { get; set; }
        public User currentUser { get; set; }
        public Coordinate userCoordinate { get; set; }

        public IndexModel(IShopRepository shopRepository, IUserRepository userRepository, UserManager<User> userManager)
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
            return Page();
        }

        public async Task<IActionResult> OnGetOrderPartial(int orderby, int filterby, int filterEQ, string number, string query)
        {
            Shop = new List<Shop>(await _shopRepository.GetAllShopsAsync());
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
            return new JsonResult(converted);
        }
    }
}
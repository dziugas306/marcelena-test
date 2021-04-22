using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;

namespace Marcelena_web.Domain.Entitites
{
    public class User : IdentityUser

    {
        public User()
        {
            Favorites = null;
        }

        public User(List<Shop> favorites)
        {
            Favorites = favorites;
        }

        public List<Shop> Favorites { get; set; }
        public Address UserAddress { get; set; }
        public Coordinate Coordinate { get; set; }
        public List<Review> Reviews { get; set; }
        public string Role { get; set; }

        public override string ToString()
        {
            return UserName;
        }

        public bool isShopFavorite(Shop isFavorite, List<Shop> Shops)
        {
            return Shops.Contains(isFavorite);
        }
    }
}
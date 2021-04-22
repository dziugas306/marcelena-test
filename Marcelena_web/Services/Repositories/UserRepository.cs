using Marcelena_web.Data;
using Marcelena_web.Domain.Entitites;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Marcelena_web.Services.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly ShopContext _context;

        public UserRepository() { }

        public UserRepository(ShopContext context)
        {
            _context = context;
        }

        public void AddUser(User user)
        {
            _context.Users.Add(user);
            _context.SaveChanges();
        }

        public async Task<User> GetUserAsync(string Id)
        {
            return await _context.Users.Include("Favorites").Include("UserAddress").Include("Coordinate").FirstOrDefaultAsync(s => s.Id == Id);
        }
        public async Task<List<User>> GetAllUserAsync()
        {
            return await _context.Users.Include("Favorites").Include("UserAddress").Include("Coordinate").ToListAsync();
        }
       

        public async Task<List<Shop>> GetAllFavoritesAsync(User currentUser)
        {
            return await _context.Shops
                .Where(p => p.Users.Contains(currentUser))
                .Include(shop => shop.Products)
                .Include(shop => shop.ShopAddress)
                .ToListAsync();
        }

            public User UpdateUser(User updatedUser)
        {
            var existingParent = _context.Users
                .Where(p => p.Id == updatedUser.Id)
                .Include(p => p.Favorites)
                .Include(p => p.Coordinate)
                .Include(p => p.UserAddress)
                .SingleOrDefault();

            if (existingParent != null)
            {
                if(existingParent.UserAddress == null)
                {
                    existingParent.Coordinate = updatedUser.Coordinate;
                    existingParent.UserAddress = updatedUser.UserAddress;
                }
                else if(existingParent.UserAddress.ToString() != updatedUser.UserAddress.ToString())
                {
                    DeleteUserAddressAndCoordinate(existingParent);
                    existingParent.Coordinate = updatedUser.Coordinate;
                    existingParent.UserAddress = updatedUser.UserAddress;
                }

                if (updatedUser.Favorites == null)
                {
                    existingParent.Favorites.Clear();
                }
                else
                {
                    foreach (var existingChild in existingParent.Favorites.ToList())
                    {
                        if (!updatedUser.Favorites.Any(c => c._id == existingChild._id))
                            existingParent.Favorites.Remove(existingChild);
                    }
                    foreach (var childUpdatedUser in updatedUser.Favorites.ToList())
                    {
                        if (!existingParent.Favorites.Any(c => c._id == childUpdatedUser._id))
                            existingParent.Favorites.Add(childUpdatedUser);
                    }
                }
                _context.SaveChanges();
            }
            return existingParent;
        }

        public void DeleteUserAddressAndCoordinate(User existingParent)
        {
            //var existingAddress = existingParent.UserAddress;
            // var existingCoordinate = existingParent.Coordinate;
            //existingParent.UserAddress = null;
            // existingParent.Coordinate = null;
            // _context.Addresses.Remove(existingAddress);
            //  _context.Coordinates.Remove(existingCoordinate);
            _context.Users.Remove(existingParent);
            _context.SaveChanges();
        }
    }

}

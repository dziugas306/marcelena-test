using Marcelena_web.Data;
using Marcelena_web.Domain.Entitites;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Marcelena_web.Services.Repositories
{
    public class ShopRepository : IShopRepository
    {
        private readonly ShopContext _context;
        public ShopRepository() { }
        public ShopRepository(ShopContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Shop>> GetAllShopsAsync()
        {
            return await _context.Shops
                .Include(shop => shop.Products)
                .Include(shop => shop.ShopAddress)
                .Include(shop => shop.Coordinate)
                .Include(shop => shop.Reviews)
                .ToListAsync();
        }

        public async Task AddShop(Shop shop)
        {
            _context.Shops.Add(shop);
            await _context.SaveChangesAsync();
        }


        public async Task<Shop> GetShopAsync(int id)
        {
            return await _context.Shops
                .Include(shop => shop.Users)
                .Include(shop => shop.Products)
                .Include(shop => shop.Reviews)
                .Include(shop => shop.ShopAddress)
                .Include(shop => shop.Coordinate)
                .Include(shop => shop.PhotoPaths)
                .SingleOrDefaultAsync(s => s._id == id);
        }

        public async Task<Shop> UpdateShopAsync(Shop updatedShop)
        {
            var existingParent = _context.Shops
                .Where(p => p._id == updatedShop._id)
                .Include(p => p.ShopAddress)
                .Include(p => p.Coordinate)
                .Include(p => p.PhotoPaths)
                .Include(p => p.Products)
                .Include(p => p.Users)
                .SingleOrDefault();

            if (existingParent != null)
            {
                _context.Entry(existingParent).CurrentValues.SetValues(updatedShop);
                existingParent.ShopAddress = updatedShop.ShopAddress;

                foreach (var existingChild in existingParent.Products.ToList())
                {
                    if (!updatedShop.Products.Any(c => c._id == existingChild._id))
                        _context.Products.Remove(existingChild);
                }

                foreach (var childupdatedShop in updatedShop.Products)
                {
                    var existingChild = existingParent.Products
                        .Where(c => c._id == childupdatedShop._id && c._id != default(int))
                        .SingleOrDefault();

                    if (existingChild != null)
                        _context.Entry(existingChild).CurrentValues.SetValues(childupdatedShop);
                    else
                    {
                        var newChild = new Product
                        {
                            ProductPrice = childupdatedShop.ProductPrice,
                            ProductType = childupdatedShop.ProductType
                        };
                        existingParent.Products.Add(newChild);
                    }
                }

                if (updatedShop.Users == null)
                {
                    existingParent.Users.Clear();
                }
                else
                {
                    foreach (var existingChild in existingParent.Users.ToList())
                    {
                        if (!updatedShop.Users.Any(c => c.Id == existingChild.Id))
                            existingParent.Users.Remove(existingChild);
                    }
                    foreach (var childUpdatedShop in updatedShop.Users.ToList())
                    {
                        if (!existingParent.Users.Any(c => c.Id == childUpdatedShop.Id))
                            existingParent.Users.Add(childUpdatedShop);
                    }
                }

                foreach (var existingChild in existingParent.PhotoPaths.ToList())
                {
                    if (!updatedShop.PhotoPaths.Any(c => c._id == existingChild._id))
                        _context.Photos.Remove(existingChild);
                }

                if (updatedShop.PhotoPaths == null)
                {
                    existingParent.PhotoPaths.Clear();
                }
                else
                {
                    foreach (var childupdatedShop in updatedShop.PhotoPaths)
                    {
                        var existingChild = existingParent.PhotoPaths
                            .Where(c => c._id == childupdatedShop._id && c._id != default(int))
                            .SingleOrDefault();

                        if (existingChild != null)
                            _context.Entry(existingChild).CurrentValues.SetValues(childupdatedShop);
                        else
                        {
                            var newChild = new Photo
                            {
                                PhotoPath = childupdatedShop.PhotoPath
                            };
                            existingParent.PhotoPaths.Add(newChild);
                        }
                    }
                }
                
                await _context.SaveChangesAsync();
            }
            return existingParent;
        }

        public void DeleteShop(int shopId)
        {
            var shop = _context.Shops
                .Include(shop => shop.Users)
                .Include(shop => shop.Products)
                .Include(shop => shop.ShopAddress)
                .Include(shop => shop.Coordinate)
                .Include(shop => shop.PhotoPaths)
                .SingleOrDefault(s => s._id == shopId);

            var address = shop.ShopAddress;
            var products = shop.Products;
            var coordinate = shop.Coordinate;
            var paths = shop.PhotoPaths;

            foreach (var item in products)
            {
                _context.Products.Remove(item);
            }
            foreach (var item in paths)
            {
                _context.Photos.Remove(item);
            }
            _context.Coordinates.Remove(coordinate);
            _context.Addresses.Remove(address);
            _context.Shops.Remove(shop);
            _context.SaveChanges();
        }        
        public void DeletePhotoPath(int photoId)
        {
            var photo = _context.Photos
                .SingleOrDefault(p => p._id == photoId);
            _context.Photos.Remove(photo);
            _context.SaveChanges();
        }

        public void DeleteReview (int reviewId)
        {
            var review = _context.Reviews
               .SingleOrDefault(p => p._id == reviewId);
            _context.Reviews.Remove(review);
            _context.SaveChanges();
        }
    }
}

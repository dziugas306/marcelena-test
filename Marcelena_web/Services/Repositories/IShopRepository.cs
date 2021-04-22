using Marcelena_web.Domain.Entitites;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Marcelena_web.Services.Repositories
{
    public interface IShopRepository
    {
        Task<IEnumerable<Shop>> GetAllShopsAsync();
        Task<Shop> GetShopAsync(int id);
        Task AddShop(Shop shop);
        Task<Shop> UpdateShopAsync(Shop shop);
        void DeleteShop(int shopId);
        void DeletePhotoPath(int photoId);
        void DeleteReview(int reviewId);
    }
}

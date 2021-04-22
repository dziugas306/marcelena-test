using Marcelena_web.Domain.Entitites;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Marcelena_web.Services.Repositories
{
    public interface IUserRepository
    {
        void AddUser(User user);
        Task<User> GetUserAsync(string Id);
        User UpdateUser(User user);
        Task<List<Shop>> GetAllFavoritesAsync(User currentUser);
        Task<List<User>> GetAllUserAsync();
        public void DeleteUserAddressAndCoordinate(User existingParent);
    }
}

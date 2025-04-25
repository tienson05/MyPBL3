using AgainPBL3.Models;
using AgainPBL3.Queries;

namespace AgainPBL3.Interfaces
{
    public interface IClientRepository : IUserRepository
    {
        Task RegisterSellerAsync(int id);

        Task UpdateUserAsync(ClientUpdateQuery user);
    }
}

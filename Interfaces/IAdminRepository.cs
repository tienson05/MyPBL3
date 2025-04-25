
using AgainPBL3.Models;
using AgainPBL3.Queries;

namespace AgainPBL3.Interfaces
{
    public interface IAdminRepository : IUserRepository
    {
        Task UpdateUserAsync(AdminUpdateQuery user);
        Task DeleteUserAsync(int id);
        Task BanUserAsync(int id);
        Task UndoBanUserAsync(int id);
    }
}

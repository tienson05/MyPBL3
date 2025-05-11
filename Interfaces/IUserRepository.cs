using AgainPBL3.Models;
using AgainPBL3.Queries;

namespace AgainPBL3.Interfaces
{
    public interface IUserRepository
    {
        Task<User> GetUserByEmailAsync(string email);
        Task<User> GetUserByIdAsync(int id);
        Task<List<User>> GetAllUserAsync();
        Task AddUserAsync(User user);
        Task UpdateLastLoginAsync(int id);
        Task<List<User>> SearchUserAsync(string keyword);
        Task ResetPasswordAsync(string newPassword, int id);
    }
}

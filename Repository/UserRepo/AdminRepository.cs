using System.ComponentModel;
using AgainPBL3.Data;
using AgainPBL3.Interfaces;
using AgainPBL3.Models;
using AgainPBL3.Queries;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;

namespace AgainPBL3.Repository.UserRepo
{
    public class AdminRepository : IAdminRepository
    {
        private readonly AppDbContext _context;

        public AdminRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<User> GetUserByEmailAsync(string email)
        {
            var user = await _context.Users
                .FirstOrDefaultAsync(u => u.Email == email);

            if (user == null) {
                return null;
            }

            var permissions = await LoadPermissions(user.UserID, user.RoleID);
            user.Permissions = permissions;
            return user;
        }

        public async Task<User> GetUserByIdAsync(int id)
        {
            var user = await _context.Users
                .FirstOrDefaultAsync(u => u.UserID == id);

            if (user == null)
            {
                return null;
            }

            var permissions = await LoadPermissions(user.UserID, user.RoleID);
            user.Permissions = permissions;
            return user;
        }

        public async Task<List<User>> GetAllUserAsync()
        {
            return await _context.Users.ToListAsync();
        }

        public async Task AddUserAsync(User user)
        {
            var ur = await _context.Users.FirstOrDefaultAsync(u => u.Email == user.Email);
            if (ur == null)
            {
                _context.Users.Add(user);
                await _context.SaveChangesAsync();
            }
        }

        public async Task UpdateLastLoginAsync(int id)
        {
            var user = await _context.Users.FindAsync(id);
            if (user != null)
            {
                user.LastLoginAt = DateTime.Now;
                await _context.SaveChangesAsync();
            }
        }

        public async Task UpdateUserAsync(AdminUpdateQuery user)
        {
            var existingUser = await _context.Users.FindAsync(user.UserID);
            if (existingUser != null)
            {
                existingUser.Username = user.Username;
                existingUser.Email = user.Email;
                existingUser.Name = user.Name;
                existingUser.Gender = user.Gender;
                existingUser.BirthOfDate = user.BirthOfDate;
                existingUser.PhoneNumber = user.PhoneNumber;
                existingUser.Address = user.Address;
                existingUser.AvatarUrl = user.AvatarUrl;
                existingUser.Status = user.Status;
                existingUser.RoleID = user.RoleID;
                existingUser.UpdatedAt = DateTime.Now;

                await _context.SaveChangesAsync();
            }
        }

        private async Task<List<int>> LoadPermissions(int userId, int roleId)
        {
            List<int> permissions = new List<int>();

            if (roleId == 4) // is Manager
            {
                permissions = await _context.UserPermissions
                    .Where(up => up.UserID == userId)
                    .Select(up => up.PermissionID)
                    .ToListAsync();
            }
            else
            {
                permissions = await _context.RolePermissions
                    .Where(rp => rp.RoleID == roleId)
                    .Select(rp => rp.PermissionID)
                    .ToListAsync();
            }

            return permissions;
        }

        public async Task DeleteUserAsync(int id)
        {
            var user = await _context.Users
                .Include(u => u.UserRatings)
                .Include(u => u.Wishlists)
                .Include(u => u.Products)
                .Include(u => u.BuyerOrders) // tai da sua o day
                .Include(u => u.VendorOrders) //
                .FirstOrDefaultAsync(u => u.UserID == id);

            _context.UserRatings.RemoveRange(user.UserRatings);
            _context.Wishlists.RemoveRange(user.Wishlists);
            _context.Products.RemoveRange(user.Products);
            _context.Orders.RemoveRange(user.BuyerOrders);// tai da sua o day
            _context.Orders.RemoveRange(user.VendorOrders);

            user.Status = "Inactive";

            await _context.SaveChangesAsync();
        }


        public async Task BanUserAsync(int id)
        {
            var user = await _context.Users.FirstOrDefaultAsync(u => u.UserID == id);

            user.Status = "Banned";

            await _context.SaveChangesAsync();
        }


        public async Task UndoBanUserAsync(int id)
        {
            var user = _context.Users.FirstOrDefault(u => u.UserID == id);

            user.Status = "Active";

            await _context.SaveChangesAsync();
        }

        public async Task<List<User>> SearchUserAsync(UserSearchQuery user)
        {
            var query = _context.Users.AsQueryable();

            if (!string.IsNullOrWhiteSpace(user.Username))
            {
                query = query.Where(u => u.Username.Contains(user.Username));
            }

            if (!string.IsNullOrWhiteSpace(user.Name))
            {
                query = query.Where(u => u.Name.Contains(user.Name));
            }

            if (!string.IsNullOrWhiteSpace(user.PhoneNumber))
            {
                query = query.Where(u => u.PhoneNumber.Contains(user.PhoneNumber));
            }

            if (!string.IsNullOrWhiteSpace(user.Email))
            {
                query = query.Where(u => u.Email.Contains(user.Email));
            }

            return await query.ToListAsync();
        }


        public async Task ResetPasswordAsync(string newPassword, int id)
        {
            var user = await _context.Users.FindAsync(id);
            if (user != null)
            {
                user.HashedPassword = newPassword;
                await _context.SaveChangesAsync();
            }
        }

    }
}

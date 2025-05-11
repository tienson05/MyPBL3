using System.Data;
using System.Data.Common;
using System.Linq.Expressions;
using System.Runtime.InteropServices;
using AgainPBL3.Data;
using AgainPBL3.Interfaces;
using AgainPBL3.Models;
using AgainPBL3.Queries;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;

namespace AgainPBL3.Repository.UserRepo
{
    public class ClientRepository : IClientRepository
    {
        public enum UserRole
        {
            Admin = 1,
            Buyer = 2,
            Seller = 3,
            Manager = 4,
        }
        private readonly AppDbContext _context;

        public ClientRepository(AppDbContext context)
        {
            _context = context;
        }
        private async Task<List<int>> LoadPermissions(int roleId)
        {         
            try
            {
                var permissions = await _context.RolePermissions
                    .Where(rp => rp.RoleID == roleId)
                    .Select(rp => rp.PermissionID)
                    .ToListAsync();

                return permissions;
            }           
            catch (Exception ex)
            {
                throw new Exception("An unexpected error occurred while loading permissions.", ex);
            }

        }
        public async Task AddUserAsync(User user)
        {
            try
            {
                _context.Users.Add(user);
                await _context.SaveChangesAsync();
            }           
            catch (Exception ex)
            {
                throw new Exception("An unexpected error occurred while loading permissions.", ex);
            }
        }

        public async Task<List<User>> GetAllUserAsync()
        {
            try
            {
                return await _context.Users.Where(u => u.RoleID != 1 && u.RoleID != 4).ToListAsync();
            }           
            catch (Exception ex)
            {
                throw new Exception("An unexpected error occurred while loading permissions.", ex);
            }
        }

        public async Task<User> GetUserByEmailAsync(string email)
        {
            var user = await _context.Users.FirstOrDefaultAsync(u => u.Email == email);
            if (user == null)
            {
                throw new InvalidOperationException($"User with email {email} not found.");
            }
            try
            {
                var permissions = await LoadPermissions(user.RoleID);
                user.Permissions = permissions;
                return user;
            }
            catch (Exception ex)
            {
                throw new Exception("An unexpected error occurred while loading.", ex);
            }
        }

        public async Task<User> GetUserByIdAsync(int id)
        {
            var user = await _context.Users.FirstOrDefaultAsync(u => u.UserID == id);
            if (user == null)
            {
                throw new InvalidOperationException($"User with email {id} not found.");
            }
            try
            {
                var permissions = await LoadPermissions(user.RoleID);
                user.Permissions = permissions;
                return user;
            }
            catch (Exception ex)
            {
                throw new Exception("An unexpected error occurred while loading.", ex);
            }
        }

        public async Task RegisterSellerAsync(int id)
        {
            var user = await _context.Users.FirstOrDefaultAsync(u => u.UserID == id);
            if (user == null)
            {
                throw new InvalidOperationException($"User not found.");
            }

            user.RoleID = (int)UserRole.Seller;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException ex)
            {
                throw new Exception("Failed to register seller due to database error.", ex);
            }
        }

        public async Task ResetPasswordAsync(string newPassword, int id)
        {
            var user = await _context.Users.FirstOrDefaultAsync(u => u.UserID == id);
            if (user == null)
            {
                throw new InvalidOperationException($"User with ID {id} not found.");
            }
            user.HashedPassword = newPassword;
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException ex)
            {
                throw new Exception("Failed to update password due to database error.", ex);
            }
        }

        public async Task<List<User>> SearchUserAsync(string keyword)
        {
            if (string.IsNullOrWhiteSpace(keyword))
                return await _context.Users
                    .Where(u => u.RoleID != (int)UserRole.Admin && u.RoleID != (int)UserRole.Manager)
                    .ToListAsync();

            keyword = keyword.Trim().ToLower();

            // Định nghĩa bộ lọc role để tái sử dụng
            Expression<Func<User, bool>> roleFilter = u =>
                u.RoleID != (int)UserRole.Admin && u.RoleID != (int)UserRole.Manager;

            // 1. Tìm theo Name
            var users = await _context.Users
                .Where(u => u.Name.ToLower().Contains(keyword))
                .Where(roleFilter)
                .ToListAsync();

            if (users.Any())
                return users;

            // 2. Tìm theo Username
            users = await _context.Users
                .Where(u => u.Username.ToLower().Contains(keyword))
                .Where(roleFilter)
                .ToListAsync();

            if (users.Any())
                return users;

            // 3. Tìm theo Email
            users = await _context.Users
                .Where(u => u.Email.ToLower().Contains(keyword))
                .Where(roleFilter)
                .ToListAsync();

            return users;
        }

        public async Task UpdateLastLoginAsync(int id)
        {
            var user = await _context.Users.FirstOrDefaultAsync(u => u.UserID == id);
            
            if (user == null)
            {
                throw new InvalidOperationException($"User not found.");
            }
            user.LastLoginAt = DateTime.UtcNow;
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (Exception ex) { 
                throw new Exception("Failed to update password due to database error", ex);
            }
        }

        public async Task UpdateUserAsync(ClientUpdateQuery user)
        {
            var existingUser = await _context.Users.FirstOrDefaultAsync(u => u.UserID == user.UserID);
            if (existingUser == null)
            {
                throw new InvalidOperationException($"User not found.");
            }

                existingUser.Username = user.Username;
                existingUser.Email = user.Email;
                existingUser.Name = user.Name;
                existingUser.Gender = user.Gender;
                existingUser.BirthOfDate = user.BirthOfDate;
                existingUser.PhoneNumber = user.PhoneNumber;
                existingUser.Address = user.Address;
                existingUser.AvatarUrl = user.AvatarUrl;
                existingUser.UpdatedAt = DateTime.UtcNow;
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw new Exception("Failed to update user due to database error", ex);
            }
        }
    }
}

using System.Data;
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
        public async Task AddUserAsync(User user)
        {
            _context.Users.Add(user);
            await _context.SaveChangesAsync();

        }

        public async Task<List<User>> GetAllUserAsync()
        {
            return await _context.Users.Where(u => u.RoleID != 1 && u.RoleID != 4).ToListAsync();
        }

        public async Task<User> GetUserByEmailAsync(string email)
        {
            var user = await _context.Users.FirstOrDefaultAsync(u => u.Email == email);
            if (user == null)
            {
                return null;
            }
            var permissions = await LoadPermissions(user.UserID, user.RoleID);
            user.Permissions = permissions;
            return user;
        }

        public async Task<User> GetUserByIdAsync(int id)
        {
            var user = await _context.Users.FirstOrDefaultAsync(u => u.UserID == id);
            if (user == null) return null;
            var permissions = await LoadPermissions(user.UserID, user.RoleID);
            user.Permissions = permissions;
            return user;
        }

        public async Task RegisterSellerAsync(int id)
        {
            var user = await _context.Users.FirstOrDefaultAsync(u => u.UserID == id);
            user.RoleID = (int)UserRole.Seller;
            await _context.SaveChangesAsync();
        }

        public async Task ResetPasswordAsync(string newPassword, int id)
        {
            var user = await _context.Users.FirstOrDefaultAsync(u => u.UserID == id);
            user.HashedPassword = newPassword;
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

        public async Task UpdateLastLoginAsync(int id)
        {
            var user = await _context.Users.FirstOrDefaultAsync(u => u.UserID == id);
            user.LastLoginAt = DateTime.Now;
            await _context.SaveChangesAsync();
        }

        public async Task UpdateUserAsync(ClientUpdateQuery user)
        {
            var existingUser = await _context.Users.FirstOrDefaultAsync(u => u.UserID == user.UserID);
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
                existingUser.UpdatedAt = DateTime.Now;

                await _context.SaveChangesAsync();
            }

        }
    }
}

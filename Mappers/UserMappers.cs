using AgainPBL3.Dtos.Account;
using AgainPBL3.Models;
using AgainPBL3.Queries;
using AgainPBL3.Services;

namespace AgainPBL3.Mapper
{
    public static class UserMappers
    {
        public static User MapToUser(this SignupDto signupDto, string hashedPassword)
        {
            return new User
            {
                Username = signupDto.Username,
                Email = signupDto.Email,
                HashedPassword = hashedPassword,
                Gender = signupDto.Gender,
                Address = signupDto.Address,
                BirthOfDate = signupDto.BirthOfDate,
                Name = signupDto.Name,
                PhoneNumber = signupDto.PhoneNumber,
                AvatarUrl = signupDto.AvatarUrl,
                CreatedAt = DateTime.Now,
                UpdatedAt = DateTime.Now,
                IsVerified = true,
                RoleID = 2,
                Balance = 0,
                TotalPosts = 0,
                TotalPurchases = 0,
                //Rating = 0,
                Status = "Active",
            };
        }

        public static ClientUpdateQuery MapToClientUpdateQuery(this UpdateUserDto updateUserDto, int id)
        {
            return new ClientUpdateQuery
            {
                UserID = id,
                Username = updateUserDto.Username,
                Email = updateUserDto.Email,
                Name = updateUserDto.Name,
                Gender = updateUserDto.Gender,
                BirthOfDate = updateUserDto.BirthOfDate,
                PhoneNumber = updateUserDto.PhoneNumber,
                Address = updateUserDto.Address,
                AvatarUrl = updateUserDto.AvatarUrl,
            };
        }
        public static User MapToUser(this CreateUserDto createUserDto, string hashedPassword)
        {
            return new User
            {
                Username = createUserDto.Username,
                Email = createUserDto.Email,
                HashedPassword = hashedPassword,
                Name = createUserDto.Name,
                Gender = createUserDto.Gender,
                BirthOfDate = createUserDto.BirthOfDate,
                PhoneNumber = createUserDto.PhoneNumber,
                Address = createUserDto.Address,
                AvatarUrl = createUserDto.AvatarUrl,
                Status = createUserDto.Status,
                RoleID = 4,
                IsVerified = createUserDto.IsVerified,
                Permissions = createUserDto.Permissions,
            };
        }

        public static AdminUpdateQuery MapToAdminUpdateQuery(this UpdateUserOfAdminDto updateUserOfAdminDto, int id)
        {
            return new AdminUpdateQuery
            {
                UserID = id,
                Username = updateUserOfAdminDto.Username,
                Email = updateUserOfAdminDto.Email,
                Name = updateUserOfAdminDto.Name,
                Gender = updateUserOfAdminDto.Gender,
                BirthOfDate = updateUserOfAdminDto.BirthOfDate,
                PhoneNumber = updateUserOfAdminDto.PhoneNumber,
                Address = updateUserOfAdminDto.Address,
                AvatarUrl = updateUserOfAdminDto.AvatarUrl,
                Status = updateUserOfAdminDto.Status,
                RoleID = updateUserOfAdminDto.Role,
                IsVerified = updateUserOfAdminDto.IsVerified,
            };
        }

        public static UserSearchQuery MapToUserSearchQuery(this SearchUserDto searchUserDto) {
            return new UserSearchQuery
            {
                Username = searchUserDto.Username,
                Email = searchUserDto.Email,
                Name = searchUserDto.Name,
                PhoneNumber = searchUserDto.PhoneNumber,
            };
        }

        public static UserViewDto MapToUserViewDto(this User user)
        {
            return new UserViewDto
            {
                UserID = user.UserID,
                Name = user.Name,
                Email = user.Email,
                PhoneNumber = user.PhoneNumber,
                Address = user.Address,
                AvatarUrl = user.AvatarUrl,
                Username = user.Username,
                Gender = user.Gender,
                BirthOfDate = user.BirthOfDate,
                Balance = user.Balance,
                TotalPosts = user.TotalPosts,
                TotalPurchases = user.TotalPurchases,
                //Rating = user.Rating,
                Role = user.RoleID,
                UpdatedAt = user.UpdatedAt,
                CreatedAt = user.CreatedAt,
                Permissions = user.Permissions,
            };
        }
    }
}

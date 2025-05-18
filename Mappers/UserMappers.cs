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
            string avatar = "data:image/jpeg;base64,/9j/4AAQSkZJRgABAQAAAQABAAD/2wCEAAkGBwgHBgkIBwgKCgkLDRYPDQwMDRsUFRAWIB0iIiAdHx8kKDQsJCYxJx8fLT0tMTU3Ojo6Iys/RD84QzQ5OjcBCgoKDQwNGg8PGjclHyU3Nzc3Nzc3Nzc3Nzc3Nzc3Nzc3Nzc3Nzc3Nzc3Nzc3Nzc3Nzc3Nzc3Nzc3Nzc3Nzc3N//AABEIAMAAzAMBIgACEQEDEQH/xAAbAAEAAgMBAQAAAAAAAAAAAAAAAQIEBQYDB//EACsQAQABAwEGBgMAAwAAAAAAAAABAgMRBAUSITFBURMiYXGBkTI0UjOh8P/EABYBAQEBAAAAAAAAAAAAAAAAAAABAv/EABYRAQEBAAAAAAAAAAAAAAAAAAARAf/aAAwDAQACEQMRAD8A+iANMgAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAEgC1q3cvV7tqiZZ9rZFyeN6uI9ga75Mw3EbIsdblclWyLOPLcqj4SjT57DOv7LvW4mbc78MKIxmKs73rCiAAAAAAAAAAAAAAAAAAHrptPXqb0UUxw5zLyb7Zmn8CxE1R5qjVxkaexRYoimiPee72xCIWZVG7HYxCQEMDXaGnUUzVRwrbBAOUqiqirdqjExzQ2e2LG7VF6mMRPNrPVrEABAAAAAAAAAAAAAkJBNmjxL1FPq6q3GKYjs5zZv71vLpKUXEgIoAAjukBh7Stxc0lcT0c9HH4dTejetV0z/LluGZiOkriaAKgAAAAAAAAAAAAAKyNn/u23SOVt3PCu0V9Yl1FFcTRExylBcIEUAAAB53Z3bVcz2ctE8avVv8Aad2KNLV3q4Q5/GFxNSAqAAAAAAAAAAAAAAImMzPq3Wx9Tv2vBrnz08mmWt11WrlNyj8oNV1UcuKWJotbRqaOkV9YZGeLKriDMAlWZMtZtLXxRE2rU8Z5z2BibW1HjX4t0T5aebBxhERxzM5meazUTQAQAAAAAAAAAAAAAAJRM46EVekippqqt1xXbndqjq2On2tVHC/Rn1hrs+/0TjtP0GN5TtfS9ZqpntNKKtraeKc0RXV8NJ7Z+kY9/wDaRWbqNp3r2aaY3KO/Vg4jjMzMyt9/SJE0hKsTGExVEtaJDIgACAAAAAAAEgJRle1auXq4otxnPOewKTmZ3aeM9oZul2XeuxFV2fDp7NppNFb09McIqq7yyt3KVWFb2bpqOM07095ZNOmsxHC3T9PTdTEYRVfCo/in6PCo/mPpcBTwqP5j6PCo/mPpcBTwqP5j6RNi3POin6egDGr0lirhNqn6Yt7ZNiv/ABzuy2WEbsFHOanQX9PPCnep7wx/+l1c0xjjDV6/Z0VRNyxHm6wtRqAnMTiYxPYUABAAAAAnkHuCbdNVddNFP5S6LQ6WnT2t3Eb082BsbTfler78G2iU1VsJVnij5RVxT5SCyEZMgsK5MglKuTILCnyfILSrMdk/JINZtPRb9Hi2488c8dWn58eXSYdVMRLQbSsTZ1MzEeWpcTWIJQqAAAAJRuzXNNMc5nA9dJTv6q3HaRXQ2bfg2aaI6QmOaZnijKQqRGTJCpEZMkKkRkyQqRGTJCpEZMkKkRkyQqRGTJCre7A2xbmvTb0flTPNm5eWqp39Pcj0IOdzkIFQAAABL32f+3R8vB7aD9y38g32eIirgjILTyFcmQWzBmO6uTILoVyZBaOaVMmQWFcmQWFcmQWFcmQWRX+FXtKMoqmNyrPaQc8g4dAAAH//2Q==";
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
                AvatarUrl = signupDto.AvatarUrl != null ? signupDto.AvatarUrl : avatar,
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

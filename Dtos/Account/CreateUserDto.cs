﻿namespace AgainPBL3.Dtos.Account
{
    public class CreateUserDto
    {
        public string Username { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
        public string Name { get; set; } = string.Empty;
        public bool Gender { get; set; } 
        public DateTime? BirthOfDate { get; set; }
        public string PhoneNumber { get; set; } = string.Empty;
        public string Address { get; set; } = string.Empty;
        public string AvatarUrl { get; set; } = string.Empty;
        public string Status { get; set; } = string.Empty;
        public int Role { get; set; } = 4;

        public List<int> Permissions { get; set; }
        public bool IsVerified { get; set; }
    }
}

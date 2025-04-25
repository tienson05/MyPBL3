namespace AgainPBL3.Dtos.Account
{
    public class UserViewDto
    {
        public int UserID { get; set; }
        public string Username { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Name { get; set; } = string.Empty;
        public bool Gender { get; set; } 
        public DateTime? BirthOfDate { get; set; }
        public string PhoneNumber { get; set; } = string.Empty;
        public string Address { get; set; } = string.Empty;
        public string AvatarUrl { get; set; } = string.Empty;
        public double Balance { get; set; }
        public int TotalPosts { get; set; }
        public int TotalPurchases { get; set; }
        public double Rating { get; set; }
        public int Role { get; set; }
        public bool IsVerified { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public DateTime? CreatedAt { get; set; }
        public List<int> Permissions { get; set; } = new List<int>();
    }
}

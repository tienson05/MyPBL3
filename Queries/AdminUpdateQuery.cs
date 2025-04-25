namespace AgainPBL3.Queries
{
    public class AdminUpdateQuery
    {
        public int UserID { get; set; }
        public string Username { get; set; } 
        public string Email { get; set; } 
        public string Name { get; set; } 
        public bool Gender { get; set; }
        public DateTime? BirthOfDate { get; set; }
        public string PhoneNumber { get; set; } 
        public string Address { get; set; }
        public string AvatarUrl { get; set; } 
        public string Status { get; set; }
        public int RoleID { get; set; }
        public bool IsVerified { get; set; }
    }
}

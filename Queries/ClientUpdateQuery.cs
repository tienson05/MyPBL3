namespace AgainPBL3.Queries
{
    public class ClientUpdateQuery
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
    }
}

namespace LibraryAPI.Models
{
    public class User
    {
        public int Id { get; set; }
        public string FirstName { get; set; } = null!;
        public string LastName { get; set; } = null!;
        public string Email { get; set; } = null!;
        public string Phone { get; set; } = null!;
        public string? Address { get; set; }
        public DateTime RegistrationDate { get; set; } = DateTime.UtcNow;
        public DateTime? MembershipExpiry { get; set; }

        public UserStatus Status { get; set; } = UserStatus.Active;

        public ICollection<Loan> Loans { get; set; } = new List<Loan>();

        public string FullName => $"{FirstName} {LastName}";
    }

    public enum UserStatus
    {
        Active = 0,
        Inactive = 1,
        Suspended = 2,
        Expired = 3
    }
}

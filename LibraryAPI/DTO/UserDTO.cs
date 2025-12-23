using LibraryAPI.DTO;

namespace LibraryAPI.DTO
{
    public class UserDto
    {
        public int Id { get; set; }
        public string FirstName { get; set; } = null!;
        public string LastName { get; set; } = null!;
        public string Email { get; set; } = null!;
        public string Phone { get; set; } = null!;
        public string? Address { get; set; }
        public DateTime RegistrationDate { get; set; }
        public DateTime? MembershipExpiry { get; set; }
        public string Status { get; set; } = null!;
        public string FullName { get; set; } = null!;

        public List<LoanDto> Loans { get; set; } = new();
    }

    public class CreateUserDto
    {
        public string FirstName { get; set; } = null!;
        public string LastName { get; set; } = null!;
        public string Email { get; set; } = null!;
        public string Phone { get; set; } = null!;
        public string? Address { get; set; }
        public DateTime? MembershipExpiry { get; set; }
    }

    public class UpdateUserDto
    {
        public string FirstName { get; set; } = null!;
        public string LastName { get; set; } = null!;
        public string Email { get; set; } = null!;
        public string Phone { get; set; } = null!;
        public string? Address { get; set; }
        public DateTime? MembershipExpiry { get; set; }
    }

    public class UpdateUserStatusDto
    {
        public string Status { get; set; } = null!;
    }
}

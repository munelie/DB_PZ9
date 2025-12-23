namespace LibraryAPI.DTO
{
    public class LoanDto
    {
        public int Id { get; set; }
        public DateTime LoanDate { get; set; }
        public DateTime DueDate { get; set; }
        public DateTime? ReturnDate { get; set; }
        public decimal? LateFee { get; set; }
        public bool IsReturned { get; set; }
        public bool IsOverdue { get; set; }

        public BookSimpleDto Book { get; set; } = null!;
        public UserSimpleDto User { get; set; } = null!;
    }

    public class CreateLoanDto
    {
        public int BookId { get; set; }
        public int UserId { get; set; }
        public DateTime DueDate { get; set; }
    }

    public class UpdateLoanDto
    {
        public DateTime? ReturnDate { get; set; }
        public decimal? LateFee { get; set; }
    }

    public class ReturnBookDto
    {
        public DateTime ReturnDate { get; set; } = DateTime.UtcNow;
        public decimal? LateFee { get; set; }
    }

    public class GenreSimpleDto
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
    }

    public class BookSimpleDto
    {
        public int Id { get; set; }
        public string Title { get; set; } = null!;
        public string ISBN { get; set; } = null!;
        public int PublicationYear { get; set; }
        public string Status { get; set; } = null!;
    }

    public class UserSimpleDto
    {
        public int Id { get; set; }
        public string FirstName { get; set; } = null!;
        public string LastName { get; set; } = null!;
        public string Email { get; set; } = null!;
        public string FullName { get; set; } = null!;
    }
}
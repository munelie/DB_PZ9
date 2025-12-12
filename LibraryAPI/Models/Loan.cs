namespace LibraryAPI.Models
{
    public class Loan
    {
        public int Id { get; set; }
        public DateTime LoanDate { get; set; } = DateTime.UtcNow;
        public DateTime DueDate { get; set; }
        public DateTime? ReturnDate { get; set; }

        public decimal? LateFee { get; set; }
        public bool IsReturned => ReturnDate.HasValue;

        public int BookId { get; set; }
        public Book Book { get; set; }

        public int UserId { get; set; }
        public User User { get; set; }
    }
}

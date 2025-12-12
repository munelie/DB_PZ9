namespace LibraryAPI.Models
{
    public class Book
    {
        public int Id { get; set; }
        public string Title { get; set; } = null!;
        public string ISBN { get; set; } = null!;
        public string? Description { get; set; }
        public int PublicationYear { get; set; }
        public int PageCount { get; set; }
        public string? Publisher { get; set; }
        public string? CoverImageUrl { get; set; }

        public int AuthorId { get; set; }
        public Author Author { get; set; }

        public int GenreId { get; set; }
        public Genre Genre { get; set; }

        public BookStatus Status { get; set; } = BookStatus.Available;

        public ICollection<Loan> Loans { get; set; } = new List<Loan>();
    }

    public enum BookStatus
    {
        Available = 0,
        OnLoan = 1,
        Reserved = 2,
        Lost = 3,
        UnderMaintenance = 4
    }
}

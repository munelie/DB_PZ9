namespace LibraryAPI.DTO
{
    public class BookDto
    {
        public int Id { get; set; }
        public string Title { get; set; } = null!;
        public string ISBN { get; set; } = null!;
        public string? Description { get; set; }
        public int PublicationYear { get; set; }
        public int PageCount { get; set; }
        public string? Publisher { get; set; }
        public string? CoverImageUrl { get; set; }
        public string Status { get; set; } = null!;

        public AuthorSimpleDto Author { get; set; } = null!;
        public GenreSimpleDto Genre { get; set; } = null!;
    }

    public class CreateBookDto
    {
        public string Title { get; set; } = null!;
        public string ISBN { get; set; } = null!;
        public string? Description { get; set; }
        public int PublicationYear { get; set; }
        public int PageCount { get; set; }
        public string? Publisher { get; set; }
        public string? CoverImageUrl { get; set; }
        public int AuthorId { get; set; }
        public int GenreId { get; set; }
    }

    public class UpdateBookDto
    {
        public string Title { get; set; } = null!;
        public string ISBN { get; set; } = null!;
        public string? Description { get; set; }
        public int PublicationYear { get; set; }
        public int PageCount { get; set; }
        public string? Publisher { get; set; }
        public string? CoverImageUrl { get; set; }
        public int AuthorId { get; set; }
        public int GenreId { get; set; }
        public string Status { get; set; } = null!;
    }
}

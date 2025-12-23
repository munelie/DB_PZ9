namespace LibraryAPI.DTO
{
    public class AuthorDto
    {
        public int Id { get; set; }
        public string FirstName { get; set; } = null!;
        public string LastName { get; set; } = null!;
        public DateTime? BirthDate { get; set; }
        public string? Biography { get; set; }
        public string? Country { get; set; }
        public string FullName { get; set; } = null!;
        public int BooksCount { get; set; }

        public List<BookSimpleDto> Books { get; set; } = new();
    }

    public class AuthorSimpleDto
    {
        public int Id { get; set; }
        public string FirstName { get; set; } = null!;
        public string LastName { get; set; } = null!;
        public string FullName { get; set; } = null!;
    }

    public class CreateAuthorDto
    {
        public string FirstName { get; set; } = null!;
        public string LastName { get; set; } = null!;
        public DateTime? BirthDate { get; set; }
        public string? Biography { get; set; }
        public string? Country { get; set; }
    }

    public class UpdateAuthorDto
    {
        public string FirstName { get; set; } = null!;
        public string LastName { get; set; } = null!;
        public DateTime? BirthDate { get; set; }
        public string? Biography { get; set; }
        public string? Country { get; set; }
    }
}

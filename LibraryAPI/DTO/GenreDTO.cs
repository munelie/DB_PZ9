using LibraryAPI.DTO;

namespace LibraryAPI.DTO
{
    public class GenreDto
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public string Description { get; set; } = string.Empty;
        public int BooksCount { get; set; }
    }

    public class GenreWithBooksDto : GenreDto
    {
        public List<BookSimpleDto> Books { get; set; } = new();
    }

    public class CreateGenreDto
    {
        public string Name { get; set; } = null!;
        public string Description { get; set; } = string.Empty;
    }

    public class UpdateGenreDto
    {
        public string Name { get; set; } = null!;
        public string Description { get; set; } = string.Empty;
    }
}

using LibraryAPI.Models;

namespace LibraryAPI.Interfaces
{
    public interface IBookRepository : IRepository<Book>
    {
        Task<IEnumerable<Book>> GetBooksByAuthorAsync(int authorId);
        Task<IEnumerable<Book>> GetBooksByGenreAsync(int genreId);
        Task<IEnumerable<Book>> GetAvailableBooksAsync();
        Task<IEnumerable<Book>> SearchBooksAsync(string searchTerm);
        Task<bool> IsBookAvailableAsync(int bookId);
    }
}

using LibraryAPI.Interfaces;
using LibraryAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace LibraryAPI.Repositories
{
    public class BookRepository : RepositoryBase<Book>, IBookRepository
    {
        public BookRepository(LibraryDbContext context) : base(context) { }

        public async Task<IEnumerable<Book>> GetBooksByAuthorAsync(int authorId)
        {
            return await _dbSet
                .Where(b => b.AuthorId == authorId)
                .Include(b => b.Author)
                .Include(b => b.Genre)
                .ToListAsync();
        }

        public async Task<IEnumerable<Book>> GetBooksByGenreAsync(int genreId)
        {
            return await _dbSet
                .Where(b => b.GenreId == genreId)
                .Include(b => b.Author)
                .Include(b => b.Genre)
                .ToListAsync();
        }

        public async Task<IEnumerable<Book>> GetAvailableBooksAsync()
        {
            return await _dbSet
                .Where(b => b.Status == BookStatus.Available)
                .Include(b => b.Author)
                .Include(b => b.Genre)
                .ToListAsync();
        }

        public async Task<IEnumerable<Book>> SearchBooksAsync(string searchTerm)
        {
            return await _dbSet
                .Where(b => b.Title.Contains(searchTerm) ||
                           b.Description.Contains(searchTerm) ||
                           b.Author.FirstName.Contains(searchTerm) ||
                           b.Author.LastName.Contains(searchTerm))
                .Include(b => b.Author)
                .Include(b => b.Genre)
                .ToListAsync();
        }

        public async Task<bool> IsBookAvailableAsync(int bookId)
        {
            var book = await _dbSet.FindAsync(bookId);
            return book != null && book.Status == BookStatus.Available;
        }
    }
}

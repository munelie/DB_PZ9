using LibraryAPI.Interfaces;
using LibraryAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace LibraryAPI.Repositories
{
    /// <summary>
    /// Репозиторий для работы с жанрами
    /// </summary>
    public class GenreRepository : RepositoryBase<Genre>, IGenreRepository
    {
        public GenreRepository(LibraryDbContext context) : base(context) { }

        /// <summary>
        /// Получить жанры с книгами
        /// </summary>
        public async Task<IEnumerable<Genre>> GetGenresWithBooksAsync()
        {
            return await _dbSet
                .Include(g => g.Books)
                .ThenInclude(b => b.Author)
                .ToListAsync();
        }

        /// <summary>
        /// Получить жанр с книгами по ID
        /// </summary>
        public async Task<Genre?> GetGenreWithBooksAsync(int id)
        {
            return await _dbSet
                .Include(g => g.Books)
                .ThenInclude(b => b.Author)
                .FirstOrDefaultAsync(g => g.Id == id);
        }

        /// <summary>
        /// Поиск жанров по названию
        /// </summary>
        public async Task<IEnumerable<Genre>> SearchGenresAsync(string searchTerm)
        {
            return await _dbSet
                .Where(g => g.Name.Contains(searchTerm) ||
                           g.Description.Contains(searchTerm))
                .ToListAsync();
        }
    }
}

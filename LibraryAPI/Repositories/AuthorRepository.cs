using LibraryAPI.Interfaces;
using LibraryAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace LibraryAPI.Repositories
{
    public class AuthorRepository : RepositoryBase<Author>, IAuthorRepository
    {
        public AuthorRepository(LibraryDbContext context) : base(context) { }

        public async Task<IEnumerable<Author>> GetAuthorsWithBooksAsync()
        {
            return await _dbSet
                .Include(a => a.Books)
                .ThenInclude(b => b.Genre)
                .ToListAsync();
        }

        public async Task<Author?> GetAuthorWithBooksAsync(int id)
        {
            return await _dbSet
                .Include(a => a.Books)
                .ThenInclude(b => b.Genre)
                .FirstOrDefaultAsync(a => a.Id == id);
        }

        public async Task<IEnumerable<Author>> SearchAuthorsAsync(string searchTerm)
        {
            return await _dbSet
                .Where(a => a.FirstName.Contains(searchTerm) ||
                           a.LastName.Contains(searchTerm) ||
                           a.Country.Contains(searchTerm))
                .Include(a => a.Books)
                .ToListAsync();
        }
    }
}

using LibraryAPI.Interfaces;
using LibraryAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace LibraryAPI.Repositories
{
    public class UserRepository : RepositoryBase<User>, IUserRepository
    {
        public UserRepository(LibraryDbContext context) : base(context) { }

        public async Task<User?> GetUserWithLoansAsync(int id)
        {
            return await _dbSet
                .Include(u => u.Loans)
                .ThenInclude(l => l.Book)
                .ThenInclude(b => b.Author)
                .FirstOrDefaultAsync(u => u.Id == id);
        }

        public async Task<IEnumerable<User>> GetActiveUsersAsync()
        {
            return await _dbSet
                .Where(u => u.Status == UserStatus.Active)
                .ToListAsync();
        }

        public async Task<bool> UserHasActiveLoansAsync(int userId)
        {
            return await _context.Loans
                .AnyAsync(l => l.UserId == userId && l.ReturnDate == null);
        }

        public async Task<User?> GetUserByEmailAsync(string email)
        {
            return await _dbSet
                .FirstOrDefaultAsync(u => u.Email == email);
        }
    }
}

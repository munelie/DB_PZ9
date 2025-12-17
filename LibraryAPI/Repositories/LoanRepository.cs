using LibraryAPI.Interfaces;
using LibraryAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace LibraryAPI.Repositories
{
    public class LoanRepository : RepositoryBase<Loan>, ILoanRepository
    {
        public LoanRepository(LibraryDbContext context) : base(context) { }

        public async Task<IEnumerable<Loan>> GetActiveLoansAsync()
        {
            return await _dbSet
                .Where(l => l.ReturnDate == null)
                .Include(l => l.Book)
                .ThenInclude(b => b.Author)
                .Include(l => l.User)
                .ToListAsync();
        }

        public async Task<IEnumerable<Loan>> GetOverdueLoansAsync()
        {
            var today = DateTime.UtcNow;
            return await _dbSet
                .Where(l => l.ReturnDate == null && l.DueDate < today)
                .Include(l => l.Book)
                .ThenInclude(b => b.Author)
                .Include(l => l.User)
                .ToListAsync();
        }

        public async Task<IEnumerable<Loan>> GetLoansByUserAsync(int userId)
        {
            return await _dbSet
                .Where(l => l.UserId == userId)
                .Include(l => l.Book)
                .ThenInclude(b => b.Author)
                .OrderByDescending(l => l.LoanDate)
                .ToListAsync();
        }

        public async Task<IEnumerable<Loan>> GetLoansByBookAsync(int bookId)
        {
            return await _dbSet
                .Where(l => l.BookId == bookId)
                .Include(l => l.User)
                .OrderByDescending(l => l.LoanDate)
                .ToListAsync();
        }

        public async Task<bool> ReturnBookAsync(int loanId)
        {
            var loan = await GetByIdAsync(loanId);
            if (loan == null || loan.ReturnDate != null)
                return false;

            loan.ReturnDate = DateTime.UtcNow;

            var book = await _context.Books.FindAsync(loan.BookId);
            if (book != null)
            {
                book.Status = BookStatus.Available;
            }

            await SaveChangesAsync();
            return true;
        }

        public async Task<decimal> CalculateLateFeeAsync(int loanId)
        {
            var loan = await GetByIdAsync(loanId);
            if (loan == null || loan.ReturnDate != null)
                return 0;

            var today = DateTime.UtcNow;
            if (loan.DueDate >= today)
                return 0;

            var daysOverdue = (today - loan.DueDate).Days;
            return daysOverdue * 50;
        }
    }
}

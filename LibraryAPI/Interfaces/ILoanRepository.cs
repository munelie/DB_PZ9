using LibraryAPI.Models;

namespace LibraryAPI.Interfaces
{
    public interface ILoanRepository : IRepository<Loan>
    {
        Task<IEnumerable<Loan>> GetActiveLoansAsync();
        Task<IEnumerable<Loan>> GetOverdueLoansAsync();
        Task<IEnumerable<Loan>> GetLoansByUserAsync(int userId);
        Task<IEnumerable<Loan>> GetLoansByBookAsync(int bookId);
        Task<bool> ReturnBookAsync(int loanId);
        Task<decimal> CalculateLateFeeAsync(int loanId);
    }
}

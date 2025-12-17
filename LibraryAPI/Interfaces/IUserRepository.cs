using LibraryAPI.Models;

namespace LibraryAPI.Interfaces
{
    public interface IUserRepository : IRepository<User>
    {
        Task<User?> GetUserWithLoansAsync(int id);
        Task<IEnumerable<User>> GetActiveUsersAsync();
        Task<bool> UserHasActiveLoansAsync(int userId);
        Task<User?> GetUserByEmailAsync(string email);
    }
}

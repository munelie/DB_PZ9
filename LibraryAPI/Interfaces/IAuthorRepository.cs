using LibraryAPI.Models;

namespace LibraryAPI.Interfaces
{
    public interface IAuthorRepository : IRepository<Author>
    {
        Task<IEnumerable<Author>> GetAuthorsWithBooksAsync();
        Task<Author?> GetAuthorWithBooksAsync(int id);
        Task<IEnumerable<Author>> SearchAuthorsAsync(string searchTerm);
    }
}

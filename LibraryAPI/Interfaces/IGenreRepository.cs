using LibraryAPI.Models;

namespace LibraryAPI.Interfaces
{
    /// <summary>
    /// Интерфейс репозитория с жанрами
    /// </summary>
    public interface IGenreRepository : IRepository<Genre>
    {
        /// <summary>
        /// Получить жанры с книгами
        /// </summary>
        Task<IEnumerable<Genre>> GetGenresWithBooksAsync();

        /// <summary>
        /// Получить жанр с книгами по айди
        /// </summary>
        Task<Genre?> GetGenreWithBooksAsync(int id);

        /// <summary>
        /// Поиск жанров по названию
        /// </summary>
        Task<IEnumerable<Genre>> SearchGenresAsync(string searchTerm);
    }
}

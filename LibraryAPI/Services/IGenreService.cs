using LibraryAPI.DTO;

namespace LibraryAPI.Services
{
    /// <summary>
    /// Интерфейс сервиса для работы с жанрами
    /// </summary>
    public interface IGenreService
    {
        /// <summary>
        /// Получить все жанры
        /// </summary>
        Task<IEnumerable<GenreDto>> GetAllGenresAsync();

        /// <summary>
        /// Получить жанр по айди
        /// </summary>
        Task<GenreDto?> GetGenreByIdAsync(int id);

        /// <summary>
        /// Создать жанр
        /// </summary>
        Task<GenreDto> CreateGenreAsync(CreateGenreDto createGenreDto);

        /// <summary>
        /// Обновить жанр
        /// </summary>
        Task<GenreDto?> UpdateGenreAsync(int id, UpdateGenreDto updateGenreDto);

        /// <summary>
        /// Удалить жанр
        /// </summary>
        Task<bool> DeleteGenreAsync(int id);

        /// <summary>
        /// Получить жанры с книгами
        /// </summary>
        Task<IEnumerable<GenreWithBooksDto>> GetGenresWithBooksAsync();

        /// <summary>
        /// Получить жанр с книгами
        /// </summary>
        Task<GenreWithBooksDto?> GetGenreWithBooksAsync(int id);

        /// <summary>
        /// Поиск жанров по названию
        /// </summary>
        Task<IEnumerable<GenreDto>> SearchGenresAsync(string searchTerm);
    }
}

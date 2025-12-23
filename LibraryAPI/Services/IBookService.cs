using LibraryAPI.DTO;

namespace LibraryAPI.Services
{
    /// <summary>
    /// Интерфейс сервиса для работы с книгами
    /// </summary>
    public interface IBookService
    {
        /// <summary>
        /// Получить все книги
        /// </summary>
        Task<IEnumerable<BookDto>> GetAllBooksAsync();

        /// <summary>
        /// Получить книгу по айди
        /// </summary>
        Task<BookDto?> GetBookByIdAsync(int id);

        /// <summary>
        /// Создать книгу
        /// </summary>
        Task<BookDto> CreateBookAsync(CreateBookDto createBookDto);

        /// <summary>
        /// Обновить книгу
        /// </summary>
        Task<BookDto?> UpdateBookAsync(int id, UpdateBookDto updateBookDto);

        /// <summary>
        /// Удалить книгу
        /// </summary>
        Task<bool> DeleteBookAsync(int id);

        /// <summary>
        /// Получить книги по автору
        /// </summary>
        Task<IEnumerable<BookDto>> GetBooksByAuthorAsync(int authorId);

        /// <summary>
        /// Получить книги по жанру
        /// </summary>
        Task<IEnumerable<BookDto>> GetBooksByGenreAsync(int genreId);

        /// <summary>
        /// Получить доступные книги
        /// </summary>
        Task<IEnumerable<BookDto>> GetAvailableBooksAsync();

        /// <summary>
        /// Поиск книг по названию, автору или описанию
        /// </summary>
        Task<IEnumerable<BookDto>> SearchBooksAsync(string searchTerm);

        /// <summary>
        /// Проверить доступность книги
        /// </summary>
        Task<bool> IsBookAvailableAsync(int bookId);
    }
}

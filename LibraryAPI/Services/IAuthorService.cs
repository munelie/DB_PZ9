using LibraryAPI.DTO;

namespace LibraryAPI.Services
{
    /// <summary>
    /// Интерфейс сервиса для работы с авторами
    /// </summary>
    public interface IAuthorService
    {
        /// <summary>
        /// Получить всех авторов
        /// </summary>
        Task<IEnumerable<AuthorDto>> GetAllAuthorsAsync();

        /// <summary>
        /// Получить автора по айди
        /// </summary>
        Task<AuthorDto?> GetAuthorByIdAsync(int id);

        /// <summary>
        /// Создать автора
        /// </summary>
        Task<AuthorDto> CreateAuthorAsync(CreateAuthorDto createAuthorDto);

        /// <summary>
        /// Обновить автора
        /// </summary>
        Task<AuthorDto?> UpdateAuthorAsync(int id, UpdateAuthorDto updateAuthorDto);

        /// <summary>
        /// Удалить автора
        /// </summary>
        Task<bool> DeleteAuthorAsync(int id);

        /// <summary>
        /// Получить авторов с их книгами
        /// </summary>
        Task<IEnumerable<AuthorDto>> GetAuthorsWithBooksAsync();

        /// <summary>
        /// Получить автора с его книгами
        /// </summary>
        Task<AuthorDto?> GetAuthorWithBooksAsync(int id);

        /// <summary>
        /// Поиск авторов по имени, фамилии или стране
        /// </summary>
        Task<IEnumerable<AuthorDto>> SearchAuthorsAsync(string searchTerm);

        /// <summary>
        /// Получить количество книг у автора
        /// </summary>
        Task<int> GetBooksCountByAuthorAsync(int authorId);
    }
}

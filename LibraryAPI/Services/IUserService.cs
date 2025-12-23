using LibraryAPI.DTO;

namespace LibraryAPI.Services
{
    /// <summary>
    /// Интерфейс сервиса для работы с пользователями
    /// </summary>
    public interface IUserService
    {
        /// <summary>
        /// Получить всех пользователей
        /// </summary>
        Task<IEnumerable<UserDto>> GetAllUsersAsync();

        /// <summary>
        /// Получить пользователя по ID
        /// </summary>
        Task<UserDto?> GetUserByIdAsync(int id);

        /// <summary>
        /// Создать пользователя
        /// </summary>
        Task<UserDto> CreateUserAsync(CreateUserDto createUserDto);

        /// <summary>
        /// Обновить пользователя
        /// </summary>
        Task<UserDto?> UpdateUserAsync(int id, UpdateUserDto updateUserDto);

        /// <summary>
        /// Удалить пользователя
        /// </summary>
        Task<bool> DeleteUserAsync(int id);

        /// <summary>
        /// Получить пользователя с его выдачами книг
        /// </summary>
        Task<UserDto?> GetUserWithLoansAsync(int id);

        /// <summary>
        /// Получить активных пользователей
        /// </summary>
        Task<IEnumerable<UserDto>> GetActiveUsersAsync();

        /// <summary>
        /// Проверить, есть ли у пользователя активные выдачи
        /// </summary>
        Task<bool> UserHasActiveLoansAsync(int userId);

        /// <summary>
        /// Получить пользователя по почте
        /// </summary>
        Task<UserDto?> GetUserByEmailAsync(string email);

        /// <summary>
        /// Обновить статус пользователя
        /// </summary>
        Task<UserDto?> UpdateUserStatusAsync(int id, UpdateUserStatusDto updateUserStatusDto);
    }
}
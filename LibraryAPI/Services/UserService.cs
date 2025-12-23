using AutoMapper;
using LibraryAPI.DTO;
using LibraryAPI.Interfaces;
using LibraryAPI.Models;

namespace LibraryAPI.Services
{
    /// <summary>
    /// Сервис для работы с пользователями
    /// </summary>
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;

        /// <summary>
        /// Конструктор сервиса пользователей
        /// </summary>
        public UserService(IUserRepository userRepository, IMapper mapper)
        {
            _userRepository = userRepository;
            _mapper = mapper;
        }

        /// <summary>
        /// Получить всех пользователей
        /// </summary>
        public async Task<IEnumerable<UserDto>> GetAllUsersAsync()
        {
            var users = await _userRepository.GetAllAsync();
            return _mapper.Map<IEnumerable<UserDto>>(users);
        }

        /// <summary>
        /// Получить пользователя по айди
        /// </summary>
        public async Task<UserDto?> GetUserByIdAsync(int id)
        {
            var user = await _userRepository.GetByIdAsync(id);
            if (user == null)
                return null;

            return _mapper.Map<UserDto>(user);
        }

        /// <summary>
        /// Создать пользователя
        /// </summary>
        public async Task<UserDto> CreateUserAsync(CreateUserDto createUserDto)
        {
            var existingUser = await _userRepository.GetUserByEmailAsync(createUserDto.Email);
            if (existingUser != null)
                throw new ArgumentException($"Ошибка");

            var user = _mapper.Map<User>(createUserDto);
            user.RegistrationDate = DateTime.UtcNow;
            user.Status = UserStatus.Active;

            var createdUser = await _userRepository.AddAsync(user);
            await _userRepository.SaveChangesAsync();

            return _mapper.Map<UserDto>(createdUser);
        }

        /// <summary>
        /// Обновить пользователя
        /// </summary>
        public async Task<UserDto?> UpdateUserAsync(int id, UpdateUserDto updateUserDto)
        {
            var user = await _userRepository.GetByIdAsync(id);
            if (user == null)
                return null;

            var existingUser = await _userRepository.GetUserByEmailAsync(updateUserDto.Email);
            if (existingUser != null && existingUser.Id != id)
                throw new ArgumentException($"Ошибка");

            _mapper.Map(updateUserDto, user);
            await _userRepository.UpdateAsync(user);
            await _userRepository.SaveChangesAsync();

            return _mapper.Map<UserDto>(user);
        }

        /// <summary>
        /// Удалить пользователя
        /// </summary>
        public async Task<bool> DeleteUserAsync(int id)
        {
            var user = await _userRepository.GetByIdAsync(id);
            if (user == null)
                return false;
            
            var hasActiveLoans = await _userRepository.UserHasActiveLoansAsync(id);
            if (hasActiveLoans)
                throw new InvalidOperationException("Ошибка");

            await _userRepository.DeleteAsync(id);
            await _userRepository.SaveChangesAsync();

            return true;
        }

        /// <summary>
        /// Получить пользователя с его выдачами книг
        /// </summary>
        public async Task<UserDto?> GetUserWithLoansAsync(int id)
        {
            var user = await _userRepository.GetUserWithLoansAsync(id);
            if (user == null)
                return null;

            return _mapper.Map<UserDto>(user);
        }

        /// <summary>
        /// Получить активных пользователей
        /// </summary>
        public async Task<IEnumerable<UserDto>> GetActiveUsersAsync()
        {
            var users = await _userRepository.GetActiveUsersAsync();
            return _mapper.Map<IEnumerable<UserDto>>(users);
        }

        /// <summary>
        /// Проверить, есть ли у пользователя активные выдачи
        /// </summary>
        public async Task<bool> UserHasActiveLoansAsync(int userId)
        {
            return await _userRepository.UserHasActiveLoansAsync(userId);
        }

        /// <summary>
        /// Получить пользователя по почте
        /// </summary>
        public async Task<UserDto?> GetUserByEmailAsync(string email)
        {
            var user = await _userRepository.GetUserByEmailAsync(email);
            if (user == null)
                return null;

            return _mapper.Map<UserDto>(user);
        }

        /// <summary>
        /// Обновить статус пользователя
        /// </summary>
        public async Task<UserDto?> UpdateUserStatusAsync(int id, UpdateUserStatusDto updateUserStatusDto)
        {
            var user = await _userRepository.GetByIdAsync(id);
            if (user == null)
                return null;

            if (Enum.TryParse<UserStatus>(updateUserStatusDto.Status, out var status))
            {
                user.Status = status;
            }
            else
            {
                throw new ArgumentException($"Invalid status value: {updateUserStatusDto.Status}");
            }

            await _userRepository.UpdateAsync(user);
            await _userRepository.SaveChangesAsync();

            return _mapper.Map<UserDto>(user);
        }
    }
}

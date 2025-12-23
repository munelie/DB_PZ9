using AutoMapper;
using LibraryAPI.DTO;
using LibraryAPI.Interfaces;
using LibraryAPI.Models;

namespace LibraryAPI.Services
{
    /// <summary>
    /// Сервис для работы с авторами
    /// </summary>
    public class AuthorService : IAuthorService
    {
        private readonly IAuthorRepository _authorRepository;
        private readonly IMapper _mapper;

        /// <summary>
        /// Конструктор сервиса авторов
        /// </summary>
        public AuthorService(IAuthorRepository authorRepository, IMapper mapper)
        {
            _authorRepository = authorRepository;
            _mapper = mapper;
        }

        /// <summary>
        /// Получить всех авторов
        /// </summary>
        public async Task<IEnumerable<AuthorDto>> GetAllAuthorsAsync()
        {
            var authors = await _authorRepository.GetAllAsync();
            return _mapper.Map<IEnumerable<AuthorDto>>(authors);
        }

        /// <summary>
        /// Получить автора по айди
        /// </summary>
        public async Task<AuthorDto?> GetAuthorByIdAsync(int id)
        {
            var author = await _authorRepository.GetByIdAsync(id);
            if (author == null)
                return null;

            return _mapper.Map<AuthorDto>(author);
        }

        /// <summary>
        /// Создать автора
        /// </summary>
        public async Task<AuthorDto> CreateAuthorAsync(CreateAuthorDto createAuthorDto)
        {
            var existingAuthor = await _authorRepository.FindAsync(a =>
                a.FirstName == createAuthorDto.FirstName &&
                a.LastName == createAuthorDto.LastName);

            if (existingAuthor.Any())
                throw new ArgumentException($"Author '{createAuthorDto.FirstName} {createAuthorDto.LastName}' already exists");

            var author = _mapper.Map<Author>(createAuthorDto);
            var createdAuthor = await _authorRepository.AddAsync(author);
            await _authorRepository.SaveChangesAsync();

            return _mapper.Map<AuthorDto>(createdAuthor);
        }

        /// <summary>
        /// Обновить автора
        /// </summary>
        public async Task<AuthorDto?> UpdateAuthorAsync(int id, UpdateAuthorDto updateAuthorDto)
        {
            var author = await _authorRepository.GetByIdAsync(id);
            if (author == null)
                return null;

            // Проверка уникальности
            var existingAuthor = await _authorRepository.FindAsync(a =>
                a.FirstName == updateAuthorDto.FirstName &&
                a.LastName == updateAuthorDto.LastName &&
                a.Id != id);

            if (existingAuthor.Any())
                throw new ArgumentException($"Another author with name '{updateAuthorDto.FirstName} {updateAuthorDto.LastName}' already exists");

            _mapper.Map(updateAuthorDto, author);
            await _authorRepository.UpdateAsync(author);
            await _authorRepository.SaveChangesAsync();

            return _mapper.Map<AuthorDto>(author);
        }

        /// <summary>
        /// Удалить автора
        /// </summary>
        public async Task<bool> DeleteAuthorAsync(int id)
        {
            var author = await _authorRepository.GetByIdAsync(id);
            if (author == null)
                return false;

            if (author.Books.Any())
                throw new InvalidOperationException("Ошибка");

            await _authorRepository.DeleteAsync(id);
            await _authorRepository.SaveChangesAsync();

            return true;
        }

        /// <summary>
        /// Получить авторов с их книгами
        /// </summary>
        public async Task<IEnumerable<AuthorDto>> GetAuthorsWithBooksAsync()
        {
            var authors = await _authorRepository.GetAuthorsWithBooksAsync();
            return _mapper.Map<IEnumerable<AuthorDto>>(authors);
        }

        /// <summary>
        /// Получить автора с его книгами
        /// </summary>
        public async Task<AuthorDto?> GetAuthorWithBooksAsync(int id)
        {
            var author = await _authorRepository.GetAuthorWithBooksAsync(id);
            if (author == null)
                return null;

            return _mapper.Map<AuthorDto>(author);
        }

        /// <summary>
        /// Поиск авторов по имени, фамилии или стране
        /// </summary>
        public async Task<IEnumerable<AuthorDto>> SearchAuthorsAsync(string searchTerm)
        {
            var authors = await _authorRepository.SearchAuthorsAsync(searchTerm);
            return _mapper.Map<IEnumerable<AuthorDto>>(authors);
        }

        /// <summary>
        /// Получить количество книг у автора
        /// </summary>
        public async Task<int> GetBooksCountByAuthorAsync(int authorId)
        {
            var author = await _authorRepository.GetAuthorWithBooksAsync(authorId);
            return author?.Books.Count ?? 0;
        }
    }
}

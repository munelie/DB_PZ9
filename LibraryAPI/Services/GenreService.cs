using AutoMapper;
using LibraryAPI.DTO;
using LibraryAPI.Interfaces;
using LibraryAPI.Models;

namespace LibraryAPI.Services
{
    /// <summary>
    /// Сервис для работы с жанрами
    /// </summary>
    public class GenreService : IGenreService
    {
        private readonly IGenreRepository _genreRepository;
        private readonly IMapper _mapper;

        /// <summary>
        /// Конструктор сервиса жанров
        /// </summary>
        public GenreService(IGenreRepository genreRepository, IMapper mapper)
        {
            _genreRepository = genreRepository;
            _mapper = mapper;
        }

        /// <summary>
        /// Получить все жанры
        /// </summary>
        public async Task<IEnumerable<GenreDto>> GetAllGenresAsync()
        {
            var genres = await _genreRepository.GetAllAsync();
            return _mapper.Map<IEnumerable<GenreDto>>(genres);
        }

        /// <summary>
        /// Получить жанр по айди
        /// </summary>
        public async Task<GenreDto?> GetGenreByIdAsync(int id)
        {
            var genre = await _genreRepository.GetByIdAsync(id);
            if (genre == null)
                return null;

            return _mapper.Map<GenreDto>(genre);
        }

        /// <summary>
        /// Создать жанр
        /// </summary>
        public async Task<GenreDto> CreateGenreAsync(CreateGenreDto createGenreDto)
        {
            var existingGenre = await _genreRepository.FindAsync(g => g.Name == createGenreDto.Name);
            if (existingGenre.Any())
                throw new ArgumentException($"Ошибка");

            var genre = _mapper.Map<Genre>(createGenreDto);
            var createdGenre = await _genreRepository.AddAsync(genre);
            await _genreRepository.SaveChangesAsync();

            return _mapper.Map<GenreDto>(createdGenre);
        }

        /// <summary>
        /// Обновить жанр
        /// </summary>
        public async Task<GenreDto?> UpdateGenreAsync(int id, UpdateGenreDto updateGenreDto)
        {
            var genre = await _genreRepository.GetByIdAsync(id);
            if (genre == null)
                return null;

            var existingGenre = await _genreRepository.FindAsync(g =>
                g.Name == updateGenreDto.Name && g.Id != id);

            if (existingGenre.Any())
                throw new ArgumentException($"Ошибка");

            _mapper.Map(updateGenreDto, genre);
            await _genreRepository.UpdateAsync(genre);
            await _genreRepository.SaveChangesAsync();

            return _mapper.Map<GenreDto>(genre);
        }

        /// <summary>
        /// Удалить жанр
        /// </summary>
        public async Task<bool> DeleteGenreAsync(int id)
        {
            var genre = await _genreRepository.GetByIdAsync(id);
            if (genre == null)
                return false;

            if (genre.Books.Any())
                throw new InvalidOperationException("Ошибка");

            await _genreRepository.DeleteAsync(id);
            await _genreRepository.SaveChangesAsync();

            return true;
        }

        /// <summary>
        /// Получить жанры с книгами
        /// </summary>
        public async Task<IEnumerable<GenreWithBooksDto>> GetGenresWithBooksAsync()
        {
            var genres = await _genreRepository.GetGenresWithBooksAsync();
            var dtos = _mapper.Map<IEnumerable<GenreWithBooksDto>>(genres);

            foreach (var dto in dtos)
            {
                dto.BooksCount = dto.Books.Count;
            }

            return dtos;
        }

        /// <summary>
        /// Получить жанр с книгами
        /// </summary>
        public async Task<GenreWithBooksDto?> GetGenreWithBooksAsync(int id)
        {
            var genre = await _genreRepository.GetGenreWithBooksAsync(id);
            if (genre == null)
                return null;

            var dto = _mapper.Map<GenreWithBooksDto>(genre);
            dto.BooksCount = dto.Books.Count;

            return dto;
        }

        /// <summary>
        /// Поиск жанров по названию
        /// </summary>
        public async Task<IEnumerable<GenreDto>> SearchGenresAsync(string searchTerm)
        {
            var genres = await _genreRepository.SearchGenresAsync(searchTerm);
            return _mapper.Map<IEnumerable<GenreDto>>(genres);
        }
    }
}

using AutoMapper;
using LibraryAPI.DTO;
using LibraryAPI.Interfaces;
using LibraryAPI.Models;

namespace LibraryAPI.Services
{
    /// <summary>
    /// Сервис для работы с книгами
    /// </summary>
    public class BookService : IBookService
    {
        private readonly IBookRepository _bookRepository;
        private readonly IAuthorRepository _authorRepository;
        private readonly IGenreRepository _genreRepository;
        private readonly IMapper _mapper;

        /// <summary>
        /// Конструктор сервиса книг
        /// </summary>
        public BookService(
            IBookRepository bookRepository,
            IAuthorRepository authorRepository,
            IGenreRepository genreRepository,
            IMapper mapper)
        {
            _bookRepository = bookRepository;
            _authorRepository = authorRepository;
            _genreRepository = genreRepository;
            _mapper = mapper;
        }

        /// <summary>
        /// Получить все книги
        /// </summary>
        public async Task<IEnumerable<BookDto>> GetAllBooksAsync()
        {
            var books = await _bookRepository.GetAllAsync();
            return _mapper.Map<IEnumerable<BookDto>>(books);
        }

        /// <summary>
        /// Получить книгу по айди
        /// </summary>
        public async Task<BookDto?> GetBookByIdAsync(int id)
        {
            var book = await _bookRepository.GetByIdAsync(id);
            if (book == null)
                return null;

            return _mapper.Map<BookDto>(book);
        }

        /// <summary>
        /// Создать книгу
        /// </summary>
        public async Task<BookDto> CreateBookAsync(CreateBookDto createBookDto)
        {
            var authorExists = await _authorRepository.ExistsAsync(createBookDto.AuthorId);
            if (!authorExists)
                throw new ArgumentException($"Такого автора не существует");

            var genreExists = await _genreRepository.ExistsAsync(createBookDto.GenreId);
            if (!genreExists)
                throw new ArgumentException($"Такого жанра нет");

            var existingBook = await _bookRepository.FindAsync(b => b.ISBN == createBookDto.ISBN);
            if (existingBook.Any())
                throw new ArgumentException($"Этот номер уже существует");

            var book = _mapper.Map<Book>(createBookDto);
            book.Status = BookStatus.Available;

            var createdBook = await _bookRepository.AddAsync(book);
            await _bookRepository.SaveChangesAsync();

            return _mapper.Map<BookDto>(createdBook);
        }

        /// <summary>
        /// Обновить книгу
        /// </summary>
        public async Task<BookDto?> UpdateBookAsync(int id, UpdateBookDto updateBookDto)
        {
            var book = await _bookRepository.GetByIdAsync(id);
            if (book == null)
                return null;

            var authorExists = await _authorRepository.ExistsAsync(updateBookDto.AuthorId);
            if (!authorExists)
                throw new ArgumentException($"Author with ID {updateBookDto.AuthorId} does not exist");

            var genreExists = await _genreRepository.ExistsAsync(updateBookDto.GenreId);
            if (!genreExists)
                throw new ArgumentException($"Genre with ID {updateBookDto.GenreId} does not exist");

            var existingBook = await _bookRepository.FindAsync(b => b.ISBN == updateBookDto.ISBN && b.Id != id);
            if (existingBook.Any())
                throw new ArgumentException($"Another book with ISBN {updateBookDto.ISBN} already exists");

            _mapper.Map(updateBookDto, book);

            if (Enum.TryParse<BookStatus>(updateBookDto.Status, out var status))
            {
                book.Status = status;
            }

            await _bookRepository.UpdateAsync(book);
            await _bookRepository.SaveChangesAsync();

            return _mapper.Map<BookDto>(book);
        }

        /// <summary>
        /// Удалить книгу
        /// </summary>
        public async Task<bool> DeleteBookAsync(int id)
        {
            var book = await _bookRepository.GetByIdAsync(id);
            if (book == null)
                return false;

            // Не выдана ли книга
            if (book.Status == BookStatus.OnLoan)
                throw new InvalidOperationException("Уже выдана");

            await _bookRepository.DeleteAsync(id);
            await _bookRepository.SaveChangesAsync();

            return true;
        }

        /// <summary>
        /// Получить книги по автору
        /// </summary>
        public async Task<IEnumerable<BookDto>> GetBooksByAuthorAsync(int authorId)
        {
            var books = await _bookRepository.GetBooksByAuthorAsync(authorId);
            return _mapper.Map<IEnumerable<BookDto>>(books);
        }

        /// <summary>
        /// Получить книги по жанру
        /// </summary>
        public async Task<IEnumerable<BookDto>> GetBooksByGenreAsync(int genreId)
        {
            var books = await _bookRepository.GetBooksByGenreAsync(genreId);
            return _mapper.Map<IEnumerable<BookDto>>(books);
        }

        /// <summary>
        /// Получить доступные книги
        /// </summary>
        public async Task<IEnumerable<BookDto>> GetAvailableBooksAsync()
        {
            var books = await _bookRepository.GetAvailableBooksAsync();
            return _mapper.Map<IEnumerable<BookDto>>(books);
        }

        /// <summary>
        /// Поиск книг по названию, автору или описанию
        /// </summary>
        public async Task<IEnumerable<BookDto>> SearchBooksAsync(string searchTerm)
        {
            var books = await _bookRepository.SearchBooksAsync(searchTerm);
            return _mapper.Map<IEnumerable<BookDto>>(books);
        }

        /// <summary>
        /// Проверить доступность книги
        /// </summary>
        public async Task<bool> IsBookAvailableAsync(int bookId)
        {
            return await _bookRepository.IsBookAvailableAsync(bookId);
        }
    }
}

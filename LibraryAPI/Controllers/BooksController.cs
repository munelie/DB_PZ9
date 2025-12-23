using AutoMapper;
using LibraryAPI.DTO;
using LibraryAPI.Services;
using Microsoft.AspNetCore.Mvc;

namespace LibraryAPI.Controllers
{
    /// <summary>
    /// Контроллер для работы с книгами
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class BooksController : ControllerBase
    {
        private readonly IBookService _bookService;
        private readonly IMapper _mapper;

        /// <summary>
        /// Конструктор контроллера книг
        /// </summary>
        public BooksController(IBookService bookService, IMapper mapper)
        {
            _bookService = bookService;
            _mapper = mapper;
        }

        /// <summary>
        /// Получить все книги
        /// </summary>
        /// <returns>Список всех книг</returns>
        [HttpGet]
        public async Task<ActionResult<ApiResponse<IEnumerable<BookDto>>>> GetAllBooks()
        {
            var books = await _bookService.GetAllBooksAsync();
            return Ok(ApiResponse<IEnumerable<BookDto>>.Ok(books));
        }

        /// <summary>
        /// Получить книгу по ID
        /// </summary>
        /// <param>ID книги</param>
        /// <returns>Информация о книге</returns>
        [HttpGet("{id}")]
        public async Task<ActionResult<ApiResponse<BookDto>>> GetBookById(int id)
        {
            var book = await _bookService.GetBookByIdAsync(id);
            if (book == null)
                return NotFound(ApiResponse<BookDto>.Error("Book not found"));

            return Ok(ApiResponse<BookDto>.Ok(book));
        }

        /// <summary>
        /// Создать новую книгу
        /// </summary>
        /// <param>Данные для создания книги</param>
        /// <returns>Созданная книга</returns>
        [HttpPost]
        public async Task<ActionResult<ApiResponse<BookDto>>> CreateBook([FromBody] CreateBookDto createBookDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ApiResponse<BookDto>.Error("Invalid data"));

            try
            {
                var book = await _bookService.CreateBookAsync(createBookDto);
                return CreatedAtAction(
                    nameof(GetBookById),
                    new { id = book.Id },
                    ApiResponse<BookDto>.Ok(book, "Book created successfully"));
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ApiResponse<BookDto>.Error(ex.Message));
            }
            catch (Exception)
            {
                return StatusCode(500, ApiResponse<BookDto>.Error("Internal server error"));
            }
        }

        /// <summary>
        /// Обновить книгу
        /// </summary>
        /// <param>ID книги</param>
        /// <param>Данные для обновления</param>
        /// <returns>Обновленная книга</returns>
        [HttpPut("{id}")]
        public async Task<ActionResult<ApiResponse<BookDto>>> UpdateBook(int id, [FromBody] UpdateBookDto updateBookDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ApiResponse<BookDto>.Error("Invalid data"));

            try
            {
                var book = await _bookService.UpdateBookAsync(id, updateBookDto);
                if (book == null)
                    return NotFound(ApiResponse<BookDto>.Error("Book not found"));

                return Ok(ApiResponse<BookDto>.Ok(book, "Book updated successfully"));
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ApiResponse<BookDto>.Error(ex.Message));
            }
            catch (Exception)
            {
                return StatusCode(500, ApiResponse<BookDto>.Error("Internal server error"));
            }
        }

        /// <summary>
        /// Удалить книгу
        /// </summary>
        /// <param>ID книги</param>
        /// <returns>Результат операции</returns>
        [HttpDelete("{id}")]
        public async Task<ActionResult<ApiResponse>> DeleteBook(int id)
        {
            try
            {
                var result = await _bookService.DeleteBookAsync(id);
                if (!result)
                    return NotFound(ApiResponse.Error("Book not found"));

                return Ok(ApiResponse.Ok("Book deleted successfully"));
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(ApiResponse.Error(ex.Message));
            }
            catch (Exception)
            {
                return StatusCode(500, ApiResponse.Error("Internal server error"));
            }
        }

        /// <summary>
        /// Поиск книг
        /// </summary>
        /// <param>Поисковый запрос</param>
        /// <returns>Найденные книги</returns>
        [HttpGet("search")]
        public async Task<ActionResult<ApiResponse<IEnumerable<BookDto>>>> SearchBooks([FromQuery] string searchTerm)
        {
            if (string.IsNullOrWhiteSpace(searchTerm))
                return BadRequest(ApiResponse<IEnumerable<BookDto>>.Error("Search term is required"));

            var books = await _bookService.SearchBooksAsync(searchTerm);
            return Ok(ApiResponse<IEnumerable<BookDto>>.Ok(books));
        }

        /// <summary>
        /// Получить доступные книги
        /// </summary>
        /// <returns>Список доступных книг</returns>
        [HttpGet("available")]
        public async Task<ActionResult<ApiResponse<IEnumerable<BookDto>>>> GetAvailableBooks()
        {
            var books = await _bookService.GetAvailableBooksAsync();
            return Ok(ApiResponse<IEnumerable<BookDto>>.Ok(books));
        }

        /// <summary>
        /// Проверить доступность книги
        /// </summary>
        /// <param>ID книги</param>
        /// <returns>Статус доступности</returns>
        [HttpGet("{id}/availability")]
        public async Task<ActionResult<ApiResponse<bool>>> CheckAvailability(int id)
        {
            var isAvailable = await _bookService.IsBookAvailableAsync(id);
            return Ok(ApiResponse<bool>.Ok(isAvailable));
        }
    }
}

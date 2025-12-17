using LibraryAPI.Interfaces;
using LibraryAPI.Models;
using Microsoft.AspNetCore.Mvc;

namespace LibraryAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TestRepositoryController : ControllerBase
    {
        private readonly IBookRepository _bookRepository;
        private readonly IAuthorRepository _authorRepository;
        private readonly IUserRepository _userRepository;
        private readonly ILoanRepository _loanRepository;

        public TestRepositoryController(
            IBookRepository bookRepository,
            IAuthorRepository authorRepository,
            IUserRepository userRepository,
            ILoanRepository loanRepository)
        {
            _bookRepository = bookRepository;
            _authorRepository = authorRepository;
            _userRepository = userRepository;
            _loanRepository = loanRepository;
        }

        [HttpGet("books")]
        public async Task<IActionResult> GetAllBooks()
        {
            var books = await _bookRepository.GetAllAsync();
            return Ok(books);
        }

        [HttpGet("books/{id}")]
        public async Task<IActionResult> GetBookById(int id)
        {
            var book = await _bookRepository.GetByIdAsync(id);
            if (book == null)
                return NotFound();
            return Ok(book);
        }

        [HttpGet("authors/with-books")]
        public async Task<IActionResult> GetAuthorsWithBooks()
        {
            var authors = await _authorRepository.GetAuthorsWithBooksAsync();
            return Ok(authors);
        }

        [HttpGet("users/{id}/with-loans")]
        public async Task<IActionResult> GetUserWithLoans(int id)
        {
            var user = await _userRepository.GetUserWithLoansAsync(id);
            if (user == null)
                return NotFound();
            return Ok(user);
        }

        [HttpGet("loans/active")]
        public async Task<IActionResult> GetActiveLoans()
        {
            var loans = await _loanRepository.GetActiveLoansAsync();
            return Ok(loans);
        }

        [HttpPost("books")]
        public async Task<IActionResult> CreateBook([FromBody] Book book)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var createdBook = await _bookRepository.AddAsync(book);
            await _bookRepository.SaveChangesAsync();
            return CreatedAtAction(nameof(GetBookById), new { id = createdBook.Id }, createdBook);
        }

        [HttpPut("books/{id}")]
        public async Task<IActionResult> UpdateBook(int id, [FromBody] Book book)
        {
            if (id != book.Id)
                return BadRequest();

            await _bookRepository.UpdateAsync(book);
            await _bookRepository.SaveChangesAsync();
            return NoContent();
        }

        [HttpDelete("books/{id}")]
        public async Task<IActionResult> DeleteBook(int id)
        {
            await _bookRepository.DeleteAsync(id);
            await _bookRepository.SaveChangesAsync();
            return NoContent();
        }
    }
}
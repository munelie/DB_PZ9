using AutoMapper;
using LibraryAPI.DTO;
using LibraryAPI.Interfaces;
using LibraryAPI.Models;

namespace LibraryAPI.Services
{
    /// <summary>
    /// Сервис для работы с выдачами книг
    /// </summary>
    public class LoanService : ILoanService
    {
        private readonly ILoanRepository _loanRepository;
        private readonly IBookRepository _bookRepository;
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;

        /// <summary>
        /// Конструктор сервиса выдач
        /// </summary>
        public LoanService(
            ILoanRepository loanRepository,
            IBookRepository bookRepository,
            IUserRepository userRepository,
            IMapper mapper)
        {
            _loanRepository = loanRepository;
            _bookRepository = bookRepository;
            _userRepository = userRepository;
            _mapper = mapper;
        }

        /// <summary>
        /// Получить все выдачи
        /// </summary>
        public async Task<IEnumerable<LoanDto>> GetAllLoansAsync()
        {
            var loans = await _loanRepository.GetAllAsync();
            return _mapper.Map<IEnumerable<LoanDto>>(loans);
        }

        /// <summary>
        /// Получить выдачу по айди
        /// </summary>
        public async Task<LoanDto?> GetLoanByIdAsync(int id)
        {
            var loan = await _loanRepository.GetByIdAsync(id);
            if (loan == null)
                return null;

            return _mapper.Map<LoanDto>(loan);
        }

        /// <summary>
        /// Создать выдачу книги
        /// </summary>
        public async Task<LoanDto> CreateLoanAsync(CreateLoanDto createLoanDto)
        {
            var book = await _bookRepository.GetByIdAsync(createLoanDto.BookId);
            if (book == null)
                throw new ArgumentException($"Ошибка");

            var user = await _userRepository.GetByIdAsync(createLoanDto.UserId);
            if (user == null)
                throw new ArgumentException($"Ошибка");

            if (book.Status != BookStatus.Available)
                throw new InvalidOperationException($"Ошибка");

            var minDueDate = DateTime.UtcNow.AddDays(1);
            var maxDueDate = DateTime.UtcNow.AddDays(60);

            if (createLoanDto.DueDate < minDueDate)
                throw new ArgumentException($"Ошибка");

            if (createLoanDto.DueDate > maxDueDate)
                throw new ArgumentException($"Ошибка");

            var loan = _mapper.Map<Loan>(createLoanDto);
            loan.LoanDate = DateTime.UtcNow;

            book.Status = BookStatus.OnLoan;
            await _bookRepository.UpdateAsync(book);

            var createdLoan = await _loanRepository.AddAsync(loan);
            await _loanRepository.SaveChangesAsync();

            return _mapper.Map<LoanDto>(createdLoan);
        }

        /// <summary>
        /// Обновить выдачу
        /// </summary>
        public async Task<LoanDto?> UpdateLoanAsync(int id, UpdateLoanDto updateLoanDto)
        {
            var loan = await _loanRepository.GetByIdAsync(id);
            if (loan == null)
                return null;

            _mapper.Map(updateLoanDto, loan);
            await _loanRepository.UpdateAsync(loan);
            await _loanRepository.SaveChangesAsync();

            return _mapper.Map<LoanDto>(loan);
        }

        /// <summary>
        /// Удалить выдачу
        /// </summary>
        public async Task<bool> DeleteLoanAsync(int id)
        {
            var loan = await _loanRepository.GetByIdAsync(id);
            if (loan == null)
                return false;

            if (loan.ReturnDate == null)
                throw new InvalidOperationException("Ошибка");

            await _loanRepository.DeleteAsync(id);
            await _loanRepository.SaveChangesAsync();

            return true;
        }

        /// <summary>
        /// Получить активные выдачи
        /// </summary>
        public async Task<IEnumerable<LoanDto>> GetActiveLoansAsync()
        {
            var loans = await _loanRepository.GetActiveLoansAsync();
            return _mapper.Map<IEnumerable<LoanDto>>(loans);
        }

        /// <summary>
        /// Получить просроченные выдачи
        /// </summary>
        public async Task<IEnumerable<LoanDto>> GetOverdueLoansAsync()
        {
            var loans = await _loanRepository.GetOverdueLoansAsync();
            return _mapper.Map<IEnumerable<LoanDto>>(loans);
        }

        /// <summary>
        /// Получить выдачи пользователя
        /// </summary>
        public async Task<IEnumerable<LoanDto>> GetLoansByUserAsync(int userId)
        {
            var loans = await _loanRepository.GetLoansByUserAsync(userId);
            return _mapper.Map<IEnumerable<LoanDto>>(loans);
        }

        /// <summary>
        /// Получить историю выдачи книги
        /// </summary>
        public async Task<IEnumerable<LoanDto>> GetLoansByBookAsync(int bookId)
        {
            var loans = await _loanRepository.GetLoansByBookAsync(bookId);
            return _mapper.Map<IEnumerable<LoanDto>>(loans);
        }

        /// <summary>
        /// Вернуть книгу
        /// </summary>
        public async Task<LoanDto?> ReturnBookAsync(int loanId)
        {
            var result = await _loanRepository.ReturnBookAsync(loanId);
            if (!result)
                return null;

            var loan = await _loanRepository.GetByIdAsync(loanId);
            return _mapper.Map<LoanDto>(loan);
        }

        /// <summary>
        /// Рассчитать штраф за просрочку
        /// </summary>
        public async Task<decimal> CalculateLateFeeAsync(int loanId)
        {
            return await _loanRepository.CalculateLateFeeAsync(loanId);
        }

        /// <summary>
        /// Выдать книгу пользователю
        /// </summary>
        public async Task<LoanDto> IssueBookAsync(int bookId, int userId, DateTime dueDate)
        {
            var createLoanDto = new CreateLoanDto
            {
                BookId = bookId,
                UserId = userId,
                DueDate = dueDate
            };

            return await CreateLoanAsync(createLoanDto);
        }
    }
}

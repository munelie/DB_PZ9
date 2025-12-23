using LibraryAPI.DTO;

namespace LibraryAPI.Services
{
    /// <summary>
    /// Интерфейс сервиса для работы с выдачами книг
    /// </summary>
    public interface ILoanService
    {
        /// <summary>
        /// Получить все выдачи
        /// </summary>
        Task<IEnumerable<LoanDto>> GetAllLoansAsync();

        /// <summary>
        /// Получить выдачу по айди
        /// </summary>
        Task<LoanDto?> GetLoanByIdAsync(int id);

        /// <summary>
        /// Создать выдачу книги
        /// </summary>
        Task<LoanDto> CreateLoanAsync(CreateLoanDto createLoanDto);

        /// <summary>
        /// Обновить выдачу
        /// </summary>
        Task<LoanDto?> UpdateLoanAsync(int id, UpdateLoanDto updateLoanDto);

        /// <summary>
        /// Удалить выдачу
        /// </summary>
        Task<bool> DeleteLoanAsync(int id);

        /// <summary>
        /// Получить активные выдачи
        /// </summary>
        Task<IEnumerable<LoanDto>> GetActiveLoansAsync();

        /// <summary>
        /// Получить просроченные выдачи
        /// </summary>
        Task<IEnumerable<LoanDto>> GetOverdueLoansAsync();

        /// <summary>
        /// Получить выдачи пользователя
        /// </summary>
        Task<IEnumerable<LoanDto>> GetLoansByUserAsync(int userId);

        /// <summary>
        /// Получить историю выдачи книги
        /// </summary>
        Task<IEnumerable<LoanDto>> GetLoansByBookAsync(int bookId);

        /// <summary>
        /// Вернуть книгу
        /// </summary>
        Task<LoanDto?> ReturnBookAsync(int loanId);

        /// <summary>
        /// Рассчитать штраф
        /// </summary>
        Task<decimal> CalculateLateFeeAsync(int loanId);

        /// <summary>
        /// Выдать книгу пользователю
        /// </summary>
        Task<LoanDto> IssueBookAsync(int bookId, int userId, DateTime dueDate);
    }
}

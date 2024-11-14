using LibraryAPI.Core.Models;

namespace LibraryAPI.Core.IServices
{
    public interface ILoanService
    {
        Task<Loan?> CreateLoan(int bookId, int userId);

        IEnumerable<Loan> GetAllLoans();

        Task<Loan?> GetLoanDetails(int id);

        Task ConfirmReturn(int loanId);

        Task<IEnumerable<Loan?>> GetLoansForReminder(DateTime reminderDate);
    }
}
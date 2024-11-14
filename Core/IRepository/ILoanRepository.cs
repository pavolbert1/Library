using LibraryAPI.Core.Models;

namespace LibraryAPI.Core.IRepository
{
    public interface ILoanRepository
    {
        Task<Loan?> AddLoan(Loan loan);

        IEnumerable<Loan> GetAllLoans();

        Task<Loan?> GetLoanDetails(int id);

        Task Update(Loan loan);

        Task Delete(int id);

        Task<IEnumerable<Loan?>> GetLoansForReminder(DateTime reminderDate);
    }
}
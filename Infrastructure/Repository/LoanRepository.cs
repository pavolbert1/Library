using LibraryAPI.Core.Exceptions;
using LibraryAPI.Core.IRepository;
using LibraryAPI.Core.Models;
using Microsoft.EntityFrameworkCore;

namespace LibraryAPI.Infrastructure.Repository
{
    public class LoanRepository(LibraryDbContext context) : ILoanRepository
    {
        private readonly LibraryDbContext _context = context;

        public async Task<Loan?> AddLoan(Loan loan)
        {
            _context.Loans.Add(loan);
            await _context.SaveChangesAsync();
            return loan;
        }

        public IEnumerable<Loan> GetAllLoans() => _context.Loans;

        public async Task<Loan?> GetLoanDetails(int id) => await _context.Loans.FindAsync(id);

        public async Task Update(Loan loan)
        {
            _context.Loans.Update(loan);
            await _context.SaveChangesAsync();
        }

        public async Task Delete(int id)
        {
            var loan = await _context.Loans.FindAsync(id) ?? throw new NotFoundException("Loan record not found.");

            _context.Loans.Remove(loan);
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<Loan?>> GetLoansForReminder(DateTime reminderDate) => 
            await _context.Loans.Where(x => !x.IsReturned && x.PlannedReturnDate.Date == reminderDate.Date).ToListAsync();
    }
}
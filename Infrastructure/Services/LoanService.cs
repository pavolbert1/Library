using LibraryAPI.Core.IRepository;
using LibraryAPI.Core.IServices;
using LibraryAPI.Core.Models;
using LibraryAPI.Core.Exceptions;

namespace LibraryAPI.Infrastructure.Services
{
    public class LoanService(ILoanRepository loanRepository, IBookRepository bookRepository, IUserRepository userRepository) : ILoanService
    {
        private readonly ILoanRepository _loanRepository = loanRepository;
        private readonly IBookRepository _bookRepository = bookRepository;
        private readonly IUserRepository _userRepository = userRepository;

        public async Task<Loan?> CreateLoan(int bookId, int userId)
        {
            var book = await _bookRepository.GetBookDetails(bookId) ??
                throw new NotFoundException("Book record not found.");

            var user = await _userRepository.GetUserDetails(userId) ??
                throw new NotFoundException("User record not found.");

            if (book.IsLoaned)
            {
                throw new BadRequestException("Book is not available now.");
            }

            book.IsLoaned = true;
            await _bookRepository.UpdateBook(book);

            var loan = new Loan
            {
                Book = book,
                User = user,
                BookId = bookId,
                UserId = userId,
                LoanDate = DateTime.Now,
                PlannedReturnDate = DateTime.Now.AddDays(30),
                IsReturned = false
            };
            await _loanRepository.AddLoan(loan);

            return loan;
        }

        public IEnumerable<Loan> GetAllLoans() => _loanRepository.GetAllLoans();

        public async Task<Loan?> GetLoanDetails(int id) => await _loanRepository.GetLoanDetails(id);

        public async Task ConfirmReturn(int loanId)
        {
            var loan = await _loanRepository.GetLoanDetails(loanId) ??
                throw new NotFoundException("Loan record not found.");

            var book = await _bookRepository.GetBookDetails(loan.BookId) ??
                throw new NotFoundException("Book record not found.");

            book.IsLoaned = false;
            await _bookRepository.UpdateBook(book);

            loan.ActualReturnDate = DateTime.Now;
            loan.IsReturned = true;
            await _loanRepository.Update(loan);
        }

        public async Task<IEnumerable<Loan?>> GetLoansForReminder(DateTime reminderDate) => await _loanRepository.GetLoansForReminder(reminderDate);
    }
}
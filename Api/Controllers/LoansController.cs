using LibraryAPI.Core.IServices;
using LibraryAPI.Core.Exceptions;
using Microsoft.AspNetCore.Mvc;

namespace LibraryAPI.Api.Controllers
{
    [ApiController]
    [Route("loans")]
    public class LoansController(ILoanService loanService) : ControllerBase
    {
        private readonly ILoanService _loanService = loanService;

        [HttpPost("create-loan")]
        public async Task<IActionResult> CreateLoan(int bookId, int userId)
        {
            if (!ModelState.IsValid)
            {
                throw new BadRequestException("Input parameters are not valid.");
            }

            var loan = await _loanService.CreateLoan(bookId, userId) ??
                throw new Exception("Loan was not created.");

            return CreatedAtAction(nameof(GetLoan), new { id = loan.Id }, loan);
        }

        [HttpPost("confirm-return")]
        public async Task<IActionResult> ConfirmReturn(int id)
        {
            if (!ModelState.IsValid)
            {
                throw new BadRequestException("Input parameter is not valid.");
            }

            await _loanService.ConfirmReturn(id);
            return Ok();
        }

        [HttpGet]
        public IActionResult GetAllLoans()
        {
            var loans = _loanService.GetAllLoans() ?? throw new NotFoundException("Loan records are not found");

            return Ok(loans);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetLoan(int id)
        {
            if (!ModelState.IsValid)
            {
                throw new BadRequestException("Input parameter is not valid.");
            }

            var loan = await _loanService.GetLoanDetails(id) ?? throw new NotFoundException("Loan record is not found");

            return Ok(loan);
        }
    }
}
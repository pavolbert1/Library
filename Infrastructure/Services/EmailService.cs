using LibraryAPI.Core.IServices;

namespace LibraryAPI.Infrastructure.Services
{
    public class EmailService() : IEmailService
    {
        public async Task SendEmailAsync(string to, string subject, string body)
        {
            await Task.Delay(500);  // Simulate email sending delay
            Console.WriteLine($"Sending email to {to} with subject: {subject}\n{body}");
        }
    }
}
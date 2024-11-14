using LibraryAPI.Core.IServices;

namespace LibraryAPI.Infrastructure.Services
{
    public class ReminderEmailService(IEmailService emailService, IServiceScopeFactory serviceScopeFactory) : BackgroundService
    {
        private readonly IEmailService _emailService = emailService;
        private readonly IServiceScopeFactory _serviceScopeFactory = serviceScopeFactory;

        private readonly TimeSpan _checkInterval = TimeSpan.FromHours(24);

        protected override async Task ExecuteAsync(CancellationToken cancellationToken)
        {
            while (!cancellationToken.IsCancellationRequested)
            {
                var reminderTask = Task.Run(CheckAndSendReminders, cancellationToken);
                var delayTask = Task.Delay(_checkInterval, cancellationToken);
                await Task.WhenAll(reminderTask, delayTask);
            }
        }

        private async Task CheckAndSendReminders()
        {
            using var scope = _serviceScopeFactory.CreateScope();
            var loanService = scope.ServiceProvider.GetRequiredService<ILoanService>();
            var bookService = scope.ServiceProvider.GetRequiredService<IBookService>();
            var userService = scope.ServiceProvider.GetRequiredService<IUserService>();

            var loans = await loanService.GetLoansForReminder(DateTime.Now.AddDays(1));

            foreach (var loan in loans)
            {
                try
                {
                    if (loan == null)
                    {
                        // log loan
                        continue;
                    }

                    var user = await userService.GetUserDetails(loan.UserId);
                    var book = await bookService.GetBookDetails(loan.BookId);

                    if (string.IsNullOrWhiteSpace(user?.Email))
                    {
                        // log loan
                        continue;
                    }

                    var email = user.Email;
                    var emailSubject = "Reminder: Book return due tomorrow";
                    var emailBody = $"Dear {user?.Name}," + Environment.NewLine + Environment.NewLine +
                                    $"This is a reminder that the book '{book?.Title}' is due for return tomorrow. Please make sure to return it on time." + Environment.NewLine + Environment.NewLine +
                                    "Thank you," + Environment.NewLine + "Your Library";

                    await _emailService.SendEmailAsync(email, emailSubject, emailBody);
                }
                catch (Exception)
                {
                    // log exception
                }
            }
        }
    }
}
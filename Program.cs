using LibraryAPI.Core.Exceptions;
using LibraryAPI.Core.IRepository;
using LibraryAPI.Core.IServices;
using LibraryAPI.Infrastructure;
using LibraryAPI.Infrastructure.Repository;
using LibraryAPI.Infrastructure.Services;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Configuration.AddJsonFile("appsettings.Development.json", optional: true)
                     .AddEnvironmentVariables();

var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

builder.Services.AddDbContext<LibraryDbContext>(options =>
    options.UseSqlServer(connectionString));

builder.Services.AddScoped<IBookRepository, BookRepository>();
builder.Services.AddScoped<ILoanRepository, LoanRepository>();
builder.Services.AddScoped<IUserRepository, UserRepository>();

builder.Services.AddScoped<IBookService, BookService>();
builder.Services.AddScoped<ILoanService, LoanService>();
builder.Services.AddScoped<IUserService, UserService>();

builder.Services.AddSingleton<IEmailService, EmailService>();
builder.Services.AddHostedService<ReminderEmailService>();

builder.Services.AddControllers(options =>
{
    options.Filters.Add(new GlobalExceptionFilter());
});

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();

    using var scope = app.Services.CreateScope();
    var dbContext = scope.ServiceProvider.GetRequiredService<LibraryDbContext>();

    if (dbContext.Database.EnsureCreated())
    {
        Console.WriteLine("Database created.");
    }
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
using Microsoft.EntityFrameworkCore;
using LibraryAPI.Models;
using LibraryAPI.Data;
using LibraryAPI.Interfaces;
using LibraryAPI.Repositories;
using LibraryAPI.Mapping;
using LibraryAPI.Middleware;
using LibraryAPI.Services;

namespace LibraryAPI
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Services.AddDbContext<LibraryDbContext>(options =>
                options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

            builder.Services.AddScoped(typeof(IRepository<>), typeof(RepositoryBase<>));
            builder.Services.AddScoped<IBookRepository, BookRepository>();
            builder.Services.AddScoped<IAuthorRepository, AuthorRepository>();
            builder.Services.AddScoped<IUserRepository, UserRepository>();
            builder.Services.AddScoped<ILoanRepository, LoanRepository>();

            builder.Services.AddScoped<IGenreRepository, GenreRepository>();

            builder.Services.AddScoped<IBookService, BookService>();
            builder.Services.AddScoped<IAuthorService, AuthorService>();
            builder.Services.AddScoped<IUserService, UserService>();
            builder.Services.AddScoped<ILoanService, LoanService>();

            builder.Services.AddAutoMapper(typeof(MappingProfile));

            builder.Services.AddControllers();
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            var app = builder.Build();

            app.UseMiddleware<ExceptionMiddleware>();

            using (var scope = app.Services.CreateScope())
            {
                var services = scope.ServiceProvider;
                try
                {
                    var context = services.GetRequiredService<LibraryDbContext>();
                    context.Database.Migrate();
                    DataSeeder.Initialize(services);
                }
                catch (Exception ex)
                {
                    var logger = services.GetRequiredService<ILogger<Program>>();
                    logger.LogError(ex, "Ошибка");
                }
            }

            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();
            app.UseAuthorization();
            app.MapControllers();

            app.Run();
        }
    }
}

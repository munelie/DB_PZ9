using LibraryAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace LibraryAPI.Data
{
    public static class DataSeeder
    {
        public static void Initialize(IServiceProvider serviceProvider)
        {
            using (var context = new LibraryDbContext(
                serviceProvider.GetRequiredService<DbContextOptions<LibraryDbContext>>()))
            {
                if (context.Authors.Any() || context.Genres.Any())
                    return;

                Console.WriteLine("Seeding database...");

                var genres = new List<Genre>
                {
                    new() { Name = "Фантастика", Description = "Научная фантастика и фэнтези" },
                    new() { Name = "Детектив", Description = "Детективные романы и триллеры" },
                    new() { Name = "Роман", Description = "Художественная литература" },
                    new() { Name = "Исторический", Description = "Исторические произведения" },
                    new() { Name = "Научно-популярный", Description = "Научная и образовательная литература" }
                };

                context.Genres.AddRange(genres);
                context.SaveChanges();

                var authors = new List<Author>
                {
                    new() { FirstName = "Айзек", LastName = "Азимов", Country = "США",
                           BirthDate = new DateTime(1920, 1, 2) },
                    new() { FirstName = "Джоан", LastName = "Роулинг", Country = "Великобритания",
                           BirthDate = new DateTime(1965, 7, 31) },
                    new() { FirstName = "Агата", LastName = "Кристи", Country = "Великобритания",
                           BirthDate = new DateTime(1890, 9, 15) },
                    new() { FirstName = "Лев", LastName = "Толстой", Country = "Россия",
                           BirthDate = new DateTime(1828, 9, 9) },
                    new() { FirstName = "Стивен", LastName = "Кинг", Country = "США",
                           BirthDate = new DateTime(1947, 9, 21) }
                };

                context.Authors.AddRange(authors);
                context.SaveChanges();

                var books = new List<Book>
                {
                    new() {
                        Title = "Я, робот",
                        ISBN = "978-5-699-21045-1",
                        AuthorId = 1,
                        GenreId = 1,
                        PublicationYear = 1950,
                        PageCount = 320,
                        Publisher = "Эксмо",
                        Description = "Классика научной фантастики",
                        Status = BookStatus.Available
                    },
                    new() {
                        Title = "Гарри Поттер и философский камень",
                        ISBN = "978-5-389-07435-4",
                        AuthorId = 2,
                        GenreId = 1,
                        PublicationYear = 1997,
                        PageCount = 432,
                        Publisher = "Махаон",
                        Description = "Первая книга о Гарри Поттере",
                        Status = BookStatus.Available
                    },
                    new() {
                        Title = "Убийство в Восточном экспрессе",
                        ISBN = "978-5-389-08261-8",
                        AuthorId = 3,
                        GenreId = 2,
                        PublicationYear = 1934,
                        PageCount = 256,
                        Publisher = "Эксмо",
                        Description = "Знаменитый детектив Агаты Кристи",
                        Status = BookStatus.OnLoan
                    },
                    new() {
                        Title = "Война и мир",
                        ISBN = "978-5-389-07191-9",
                        AuthorId = 4,
                        GenreId = 3,
                        PublicationYear = 1869,
                        PageCount = 1225,
                        Publisher = "АСТ",
                        Description = "Роман-эпопея Льва Толстого",
                        Status = BookStatus.Available
                    },
                    new() {
                        Title = "Оно",
                        ISBN = "978-5-17-090768-2",
                        AuthorId = 5,
                        GenreId = 1,
                        PublicationYear = 1986,
                        PageCount = 1248,
                        Publisher = "АСТ",
                        Description = "Роман ужасов Стивена Кинга",
                        Status = BookStatus.Available
                    }
                };

                context.Books.AddRange(books);
                context.SaveChanges();

                var users = new List<User>
                {
                    new() {
                        FirstName = "Иван",
                        LastName = "Иванов",
                        Email = "ivan@gmail.com",
                        Phone = "+79991234567",
                        Address = "ул. Пушкина, д. 1",
                        Status = UserStatus.Active
                    },
                    new() {
                        FirstName = "Мария",
                        LastName = "Петрова",
                        Email = "maria@gmail.com",
                        Phone = "+79997654321",
                        Address = "ул. Лермонтова, д. 10",
                        Status = UserStatus.Active
                    },
                    new() {
                        FirstName = "Алексей",
                        LastName = "Сидоров",
                        Email = "alex@gmail.com",
                        Phone = "+79998887766",
                        Address = "пр. Мира, д. 25",
                        Status = UserStatus.Active
                    },
                    new() {
                        FirstName = "Елена",
                        LastName = "Козлова",
                        Email = "elena@gmail.com",
                        Phone = "+79995554433",
                        Address = "ул. Садовая, д. 5",
                        Status = UserStatus.Active
                    },
                    new() {
                        FirstName = "Дмитрий",
                        LastName = "Смирнов",
                        Email = "dmitry@gmail.com",
                        Phone = "+79993332211",
                        Address = "ул. Центральная, д. 15",
                        Status = UserStatus.Active
                    }
                };

                context.Users.AddRange(users);
                context.SaveChanges();

                var loans = new List<Loan>
                {
                    new() {
                        BookId = 1,
                        UserId = 1,
                        LoanDate = DateTime.UtcNow.AddDays(-10),
                        DueDate = DateTime.UtcNow.AddDays(20)
                    },
                    new() {
                        BookId = 3,
                        UserId = 2,
                        LoanDate = DateTime.UtcNow.AddDays(-5),
                        DueDate = DateTime.UtcNow.AddDays(25)
                    },
                    new() {
                        BookId = 2,
                        UserId = 3,
                        LoanDate = DateTime.UtcNow.AddDays(-15),
                        DueDate = DateTime.UtcNow.AddDays(5),
                        ReturnDate = DateTime.UtcNow.AddDays(-1)
                    },
                    new() {
                        BookId = 4,
                        UserId = 4,
                        LoanDate = DateTime.UtcNow.AddDays(-3),
                        DueDate = DateTime.UtcNow.AddDays(27)
                    },
                    new() {
                        BookId = 5,
                        UserId = 5,
                        LoanDate = DateTime.UtcNow.AddDays(-20),
                        DueDate = DateTime.UtcNow.AddDays(0),
                        LateFee = 150.50m
                    }
                };

                context.Loans.AddRange(loans);
                context.SaveChanges();

                Console.WriteLine("Успех");
            }
        }
    }
}
using IoC.WPF.Data.Context;
using IoC.WPF.Data.Entityes;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IoC.WPF.Data
{
    internal class DbInitializer
    {
        private readonly BookinistDB _db;
        private readonly ILogger<DbInitializer> _logger;

        public DbInitializer(BookinistDB db, ILogger<DbInitializer> logger)
        {
            _db = db;
            _logger = logger;
        }
        public async Task InitializeAsync()
        {
            var timer = Stopwatch.StartNew();
            _logger.LogInformation($"Инициализация БД...");

            _logger.LogInformation($"Удаление существующей БД...");
            await _db.Database.EnsureDeletedAsync().ConfigureAwait(false);
            _logger.LogInformation($"Удаление существующей БД выполнено за {timer.ElapsedMilliseconds} мс");

            _logger.LogInformation($"Миграция БД...");
            await _db.Database.MigrateAsync().ConfigureAwait(false);
            _logger.LogInformation($"Миграция БД выполнена за {timer.ElapsedMilliseconds} мс");

            if (await _db.Books.AnyAsync()) return;

            await InitializeCategories();
            await InitializeBooks();
            await InitializeSellers();
            await InitializeBuyers();
            await InitializeDeals();
            _logger.LogInformation($"Инициализация БД выполнена за {timer.Elapsed.TotalSeconds} с");

        }

        private const int RECORD_COUNT = 12;
        private Category[] _categories;
        private async Task InitializeCategories()
        {
            var timer = Stopwatch.StartNew();
            _logger.LogInformation($"Инициализация категорий...");

            _categories = new Category[RECORD_COUNT];
            for (int i = 0; i < RECORD_COUNT; i++)
            {
                _categories[i] = new Category { Name = $"Категория {i + 1}" };
            }
            await _db.Categories.AddRangeAsync(_categories);
            await _db.SaveChangesAsync();

            _logger.LogInformation($"Инициализация категорий выполнена за {timer.ElapsedMilliseconds} мс");
        }

        private Book[] _books;
        private async Task InitializeBooks()
        {
            var timer = Stopwatch.StartNew();
            _logger.LogInformation($"Инициализация книг...");

            var rnd = new Random();
            _books = Enumerable.Range(1, RECORD_COUNT)
                .Select(i => new Book 
                {
                    Name = $"Книга {i}",
                    Category = rnd.NextItem(_categories)
                })
                .ToArray();
            await _db.Books.AddRangeAsync(_books);
            await _db.SaveChangesAsync();

            _logger.LogInformation($"Инициализация книг выполнена за {timer.ElapsedMilliseconds} мс");
        }

        private Seller[] _sellers;
        private async Task InitializeSellers()
        {
            var timer = Stopwatch.StartNew();
            _logger.LogInformation($"Инициализация продавцов...");

            _sellers = Enumerable.Range(1, RECORD_COUNT)
                .Select(i => new Seller 
                {
                    Name = $"Продавец-Имя {i}",
                    Surname = $"Продавец-Фамилия {i}",
                    Patronymic = $"Продавец-Отчество {i}",
                })
                .ToArray();
            await _db.Sellers.AddRangeAsync(_sellers);
            await _db.SaveChangesAsync();
            
            _logger.LogInformation($"Инициализация продавцов выполнена за {timer.ElapsedMilliseconds} мс");
        }
        private Buyer[] _buyers;
        private async Task InitializeBuyers()
        {
            var timer = Stopwatch.StartNew();
            _logger.LogInformation($"Инициализация покупателей...");

            _buyers = Enumerable.Range(1, RECORD_COUNT)
                .Select(i => new Buyer 
                {
                    Name = $"Продавец-Имя {i}",
                    Surname = $"Продавец-Фамилия {i}",
                    Patronymic = $"Продавец-Отчество {i}",
                })
                .ToArray();
            await _db.Buyers.AddRangeAsync(_buyers);
            await _db.SaveChangesAsync();
            
            _logger.LogInformation($"Инициализация покупателей выполнена за {timer.ElapsedMilliseconds} мс");
        }

        private const int DEALS_COUNT = 1000;
        private async Task InitializeDeals()
        {
            var timer = Stopwatch.StartNew();
            _logger.LogInformation($"Инициализация сделок...");

            var rnd = new Random();
            var details = Enumerable.Range(1, DEALS_COUNT)
                .Select(i => new Deal
                {
                    Book = rnd.NextItem(_books),
                    Seller = rnd.NextItem(_sellers),
                    Buyer = rnd.NextItem(_buyers),
                    Price = (decimal)(rnd.NextDouble() * 500)
                });
            await _db.Deals.AddRangeAsync(details);
            await _db.SaveChangesAsync();
            
            _logger.LogInformation($"Инициализация сделок выполнена за {timer.ElapsedMilliseconds} мс");
        }
    }
}

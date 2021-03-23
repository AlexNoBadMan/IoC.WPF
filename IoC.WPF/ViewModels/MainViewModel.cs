using IoC.WPF.Data.Entityes;
using IoC.WPF.Data.Interfaces;
using IoC.WPF.Models;
using MathCore.WPF.Commands;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace IoC.WPF.ViewModels
{
    internal class MainViewModel : ViewModelBase
    {
        private string _title = "Test1";
        private ICommand _testCommand;
        private readonly IRepository<Book> _books;
        private readonly IRepository<Deal> _deals;

        public ObservableCollection<BestSellerInfo> Bestsellers { get; } = new ObservableCollection<BestSellerInfo>();
        public string Title { get => _title; set => Set(ref _title, value); }
        public ICommand TestCommand => _testCommand
            ??= new LambdaCommandAsync(OnTestCommandExecuted);

        private async Task OnTestCommandExecuted()
        {
            var bestsellers_query = _deals.Items
               .GroupBy(b => b.Book.Id)
               .Select(deals => new { BookId = deals.Key, Count = deals.Count(), Sum = deals.Sum(d => d.Price) })
               .OrderByDescending(deals => deals.Count)
               .Take(5)
               .Join(_books.Items,
                    deals => deals.BookId,
                    book => book.Id,
                    (deals, book) => new BestSellerInfo
                    {
                        Book = book.Name,
                        SellCount = deals.Count,
                        SumCost = deals.Sum
                    });
            Bestsellers.Clear();
            foreach (var item in await bestsellers_query.ToArrayAsync())
            {
                Bestsellers.Add(item);
            }
        }

        public MainViewModel(IRepository<Book> books,
            IRepository<Seller> Sellers,
            IRepository<Buyer> Buyers,
            IRepository<Deal> deals)
        {
            _books = books;
            _deals = deals;
        }
    }
}

using IoC.WPF.Data.Context;
using IoC.WPF.Data.Entityes;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace IoC.WPF.Data
{
    class BooksRepository : DbRepository<Book>
    {
        public override IQueryable<Book> Items => base.Items.Include(item => item.Category);

        public BooksRepository(BookinistDB db) : base(db) { }
    }
}

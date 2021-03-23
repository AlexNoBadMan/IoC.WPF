using IoC.WPF.Data.Context;
using IoC.WPF.Data.Entityes;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace IoC.WPF.Data
{
    class DealsRepository : DbRepository<Deal>
    {
        public override IQueryable<Deal> Items => base.Items
           .Include(item => item.Book)
           .Include(item => item.Seller)
           .Include(item => item.Buyer)
        ;

        public DealsRepository(BookinistDB db) : base(db) { }
    }
}

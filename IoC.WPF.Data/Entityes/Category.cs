using IoC.WPF.Data.Entityes.Base;
using System.Collections.Generic;

namespace IoC.WPF.Data.Entityes
{
    public class Category : NamedEntity
    {
        public virtual ICollection<Book> Books { get; set; }
    }
}
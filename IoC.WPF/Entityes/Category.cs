using IoC.WPF.Entityes.Base;
using System.Collections.Generic;

namespace IoC.WPF.Entityes
{
    public class Category : NamedEntity
    {
        public virtual ICollection<Book> Books { get; set; }
    }
}
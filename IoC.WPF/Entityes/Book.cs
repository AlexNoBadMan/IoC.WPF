using IoC.WPF.Entityes.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IoC.WPF.Entityes
{
    public class Book : NamedEntity
    {
        public virtual Category Category { get; set; }
    }
}

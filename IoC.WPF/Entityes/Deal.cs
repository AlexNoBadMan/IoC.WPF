using IoC.WPF.Entityes.Base;
using System.ComponentModel.DataAnnotations.Schema;

namespace IoC.WPF.Entityes
{
    public class Deal : Person 
    { 
        [Column(TypeName ="decimal(18,2)")]
        public decimal Price { get; set; }
        public virtual Book Book { get; set; }
        public virtual Seller Seller { get; set; }
        public virtual Buyer Buyer { get; set; }
    }
}
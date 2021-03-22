using System.ComponentModel.DataAnnotations;

namespace IoC.WPF.Entityes.Base
{
    public abstract class NamedEntity : Entity
    {
        [Required]
        public string Name { get; set; }
    }
}

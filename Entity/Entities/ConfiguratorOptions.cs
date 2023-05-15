using System.ComponentModel.DataAnnotations;

namespace KursovaWork.Entity.Entities
{
    public class ConfiguratorOptions
    {
        public int Id { get; set; }

        [Required]
        public int OrderId { get; set; }

        [StringLength(50)]
        public string? Color { get; set; }

        [StringLength(50)]
        public string? Transmission { get; set; }

        [StringLength(50)]
        public string? FuelType { get; set; }

        public virtual Order? Order { get; set; }
    }
}

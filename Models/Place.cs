using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;

namespace TravelManagement.Models
{
    public class Place
    {
        [Key]
        public int PlaceId { get; set; }

        [Required(ErrorMessage ="Введите название места")]
        [StringLength(100)]
        public string PlaceName { get; set; } = string.Empty;

        [StringLength(1000)]
        public string? PlaceDescription { get; set; }

        [Column(TypeName = "decimal(10,2)")]
        public decimal BaseCost { get; set; }

        //идентификатор страны, к которой относится место
        public int CountryId { get; set; }

        //связь с объектом страны
        [ValidateNever]
        public Country Country { get; set; } = null!;
    }
}

using System.ComponentModel.DataAnnotations;
namespace TravelManagement.Models
{
    public class Country
    {
        [Key]
        public int CountryId { get; set; }

        [Required(ErrorMessage ="Введите название страны")]
        [StringLength(100)]
        public string CountryName { get; set; } = string.Empty;

        [StringLength(1000)]
        public string? CountryDescription { get; set; }

        //список мест, которые есть в стране
        public ICollection<Place> Places { get; set; } = new List<Place>();
    }
}

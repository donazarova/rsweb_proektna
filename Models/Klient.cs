using System.ComponentModel.DataAnnotations;

namespace MVCTurizam.Models
{
    public class Klient
    {
        public int Id { get; set; }

        [StringLength(30, MinimumLength = 6)]
        [Display(Name = "Ime i Prezime")]
        [Required]
        public string ImePrezime { get; set; }

        [DataType(DataType.EmailAddress)]
        [Display(Name = "E-mail Adresa")]
        public string? Email { get; set; }
        public int? Telefon { get; set; }
        public string? SlikaOdPasos { get; set; }

        public ICollection<Patuvanje>? Patuvanje { get; set; }
    }
}

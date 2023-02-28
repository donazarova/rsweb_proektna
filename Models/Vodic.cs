using System.ComponentModel.DataAnnotations;

namespace MVCTurizam.Models
{
    public class Vodic
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

        [Display(Name = "Godini Iskustvo")]
        public int? Iskustvo { get; set; }

        public ICollection<Destinacija>? Destinacijas { get; set; }
    }
}

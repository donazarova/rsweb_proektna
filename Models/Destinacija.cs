using System.ComponentModel.DataAnnotations;

namespace MVCTurizam.Models
{
    public class Destinacija
    {
        public int Id { get; set; }
        public string Ime { get; set; }
        public string? Drzava { get; set; }
        public string? Kontinent { get; set; }
        public decimal? Dalecina { get; set; }
        public decimal? Temperatura { get; set; }

        [DataType(DataType.Currency)]
        [Display(Name = "Cena na Povratna Karta")]
        public decimal? CenaKarta { get; set; }
        public string? SlikaOdDestinacija { get; set; }

        [Display(Name = "Vodic")]
        public int? VodicId { get; set; }
        public Vodic? Vodic { get; set; }
        public ICollection<Patuvanje>? Patuvanje { get; set; }

    }
}

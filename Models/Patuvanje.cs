using System.ComponentModel.DataAnnotations;

namespace MVCTurizam.Models
{
    public class Patuvanje
    {
        public int Id { get; set; }
        public int KlientId { get; set; }
        public Klient? Klient { get; set; }
        public int DestinacijaId { get; set; }
        public Destinacija? Destinacija { get; set; }

        [Display(Name = "Datum na Poaganje")]
        [DataType(DataType.Date)]
        public DateTime? DatumOd { get; set; }

        [Display(Name = "Datum na Pristiganje")]
        [DataType(DataType.Date)]
        public DateTime? DatumDo { get; set; }
    }
}

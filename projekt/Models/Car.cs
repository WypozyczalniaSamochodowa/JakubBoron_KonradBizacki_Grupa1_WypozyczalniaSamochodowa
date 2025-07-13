using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace projekt.Models
{
    public class Car
    {
        public int Id { get; set; }

        [Required]
        public string Marka { get; set; }

        [Required]
        public string Model { get; set; }

        [Range(0, 10000)]
        public decimal CenaZaDzien { get; set; }

        public StatusSamochodu Status { get; set; }

        public string Zdjecie { get; set; }
        public string Silnik { get; set; }
        public string Spalanie { get; set; }
        public int MocKM { get; set; }
        public decimal PrzyspieszenieSek { get; set; }
        public string Naped { get; set; }
        public string Opis { get; set; }

        public ICollection<Wypozyczenie> Wypozyczenia { get; set; } // nawigacja do wypożyczeń
    }

    public enum StatusSamochodu
    {
        Dostepny,
        Wypozyczony,
        Serwis
    }
}

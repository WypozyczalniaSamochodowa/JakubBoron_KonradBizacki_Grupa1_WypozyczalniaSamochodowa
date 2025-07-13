using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace projekt.Models
{
    public class Wypozyczenie
    {
        public int Id { get; set; }

        public int KlientId { get; set; }
        public Klient Klient { get; set; }    // nawigacja do klienta

        public int CarId { get; set; }
        public Car Car { get; set; }          // nawigacja do auta

        public DateTime DataOd { get; set; }
        public DateTime DataDo { get; set; }

        public int Dni => (DataDo - DataOd).Days;
        public decimal CenaCalkowita { get; set; }
    }
}

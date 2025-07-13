using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace projekt.Models
{
    public class Wypozyczenie
    {
        public int Id { get; set; }

        public int KlientId { get; set; }
        public Klient Klient { get; set; }   

        public int CarId { get; set; }
        public Car Car { get; set; }      

        public DateTime DataOd { get; set; }
        public DateTime DataDo { get; set; }

        public int Dni => (DataDo - DataOd).Days;
        public decimal CenaCalkowita { get; set; }
    }
}

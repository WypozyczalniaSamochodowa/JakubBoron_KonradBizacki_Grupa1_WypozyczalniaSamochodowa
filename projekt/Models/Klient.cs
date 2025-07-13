using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace projekt.Models
{
    public class Klient
    {
        public string Id { get; set; }

        [Required]
        public string Imie { get; set; }

        [Required]
        public string Nazwisko { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        [Phone]
        public string Telefon { get; set; }

        // Kolekcja wypożyczeń klienta
        public ICollection<Wypozyczenie> Wypozyczenia { get; set; } = new List<Wypozyczenie>();
    }
}
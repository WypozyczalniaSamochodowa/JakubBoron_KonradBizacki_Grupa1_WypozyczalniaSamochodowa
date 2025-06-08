namespace projekt.Models
{
    public class Car
    {
        public int Id { get; set; }
        public string Marka { get; set; }
        public string Model { get; set; }
        public decimal CenaZaDzien { get; set; }
        public bool Dostepny { get; set; }

        public string Silnik { get; set; }
        public string Spalanie { get; set; }
        public int MocKM { get; set; }
        public decimal PrzyspieszenieSek { get; set; }
        public string Naped { get; set; }

        public List<Wypozyczenie> Wypozyczenia { get; set; } = new();
    }
}

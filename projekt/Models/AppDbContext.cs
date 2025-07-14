using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using projekt.Models;
using System.Linq;

namespace projekt.Data
{
    public class AppDbContext : IdentityDbContext<IdentityUser>
    {
        public DbSet<Car> Samochody { get; set; }
        public DbSet<Klient> Klienci { get; set; }
        public DbSet<Wypozyczenie> Wypozyczenia { get; set; }

        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Wypozyczenie>()
                .HasOne(w => w.Klient)
                .WithMany(k => k.Wypozyczenia)
                .HasForeignKey(w => w.KlientId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Wypozyczenie>()
                .HasOne(w => w.Car)
                .WithMany(c => c.Wypozyczenia)
                .HasForeignKey(w => w.CarId)
                .OnDelete(DeleteBehavior.Restrict);

 
            modelBuilder.Entity<Klient>()
                .HasKey(k => k.Id);

            modelBuilder.Entity<Klient>()
                .Property(k => k.Id)
                .ValueGeneratedOnAdd();
        }

        public void Seed
            ()
        {
            if (!Samochody.Any())
            {
                Samochody.AddRange(
                    new Car
                    {
                        Marka = "Ferrari",
                        Model = "488 GTB",
                        CenaZaDzien = 1500,
                        Status = StatusSamochodu.Dostepny,
                        Zdjecie = "ferrari.jpg",
                        Silnik = "3.9L V8 Twin-Turbo",
                        Spalanie = "11.4L/100km",
                        MocKM = 670,
                        PrzyspieszenieSek = 3.0m,
                        Naped = "RWD",
                        Opis = "Ferrari 488 GTB to esencja włoskiej perfekcji motoryzacyjnej. Z silnikiem o mocy 670 KM katapultuje się do setki w zaledwie 3 sekundy. Agresywny design i precyzja prowadzenia czynią z niego idealny samochód na tor i drogę. Każde muśnięcie gazu budzi dźwięk prawdziwego superauta."
                    },
                    new Car
                    {
                        Marka = "Lamborghini",
                        Model = "Huracán Evo",
                        CenaZaDzien = 1700,
                        Status = StatusSamochodu.Dostepny,
                        Zdjecie = "huracan.jpg",
                        Silnik = "5.2L V10",
                        Spalanie = "13.7L/100km",
                        MocKM = 640,
                        PrzyspieszenieSek = 2.9m,
                        Naped = "AWD",
                        Opis = "Huracán Evo to potwór na kołach z nieziemskim brzmieniem wolnossącego V10. Jego przyspieszenie do 100 km/h w mniej niż 3 sekundy zapiera dech. Napęd na cztery koła gwarantuje absolutną trakcję w każdych warunkach. To nie tylko samochód – to adrenalina w czystej postaci."
                    },
                    new Car
                    {
                        Marka = "Porsche",
                        Model = "911 Turbo S",
                        CenaZaDzien = 1400,
                        Status = StatusSamochodu.Dostepny,
                        Zdjecie = "porsche.jpg",
                        Silnik = "3.8L Twin-Turbo",
                        Spalanie = "11.0L/100km",
                        MocKM = 650,
                        PrzyspieszenieSek = 2.7m,
                        Naped = "AWD",
                        Opis = "Porsche 911 Turbo S to legenda o wyrafinowanej sile. Oferuje brutalne przyspieszenie przy zachowaniu niemieckiej elegancji i precyzji. Ten model redefiniuje pojęcie codziennego superauta. Potrafi być zarówno komfortowy, jak i bezlitosny na autostradzie czy torze."
                    },
                    new Car
                    {
                        Marka = "Aston Martin",
                        Model = "DBS Superleggera",
                        CenaZaDzien = 1600,
                        Status = StatusSamochodu.Dostepny,
                        Zdjecie = "aston.jpg",
                        Silnik = "5.2L V12 Twin-Turbo",
                        Spalanie = "12.0L/100km",
                        MocKM = 725,
                        PrzyspieszenieSek = 3.4m,
                        Naped = "RWD",
                        Opis = "DBS Superleggera to brytyjski arystokrata o brutalnej duszy. 725 koni mechanicznych z V12 sprawia, że to nie jest auto dla początkujących. Jego obecność przyciąga spojrzenia, a dźwięk silnika – ciarki na plecach. Łączy luksus z ekstremalnymi osiągami w idealnej harmonii."
                    },
                    new Car
                    {
                        Marka = "McLaren",
                        Model = "720S",
                        CenaZaDzien = 1800,
                        Status = StatusSamochodu.Dostepny,
                        Zdjecie = "mclaren.jpg",
                        Silnik = "4.0L V8 Twin-Turbo",
                        Spalanie = "12.2L/100km",
                        MocKM = 720,
                        PrzyspieszenieSek = 2.9m,
                        Naped = "RWD",
                        Opis = "McLaren 720S to technologiczne dzieło sztuki, stworzone na potrzeby toru. Jego niesamowita aerodynamika i 720 KM zapewniają doznania nie z tej ziemi. Zbudowany niemal w całości z włókna węglowego, jest lekki i zabójczo szybki. Kierowca staje się tu częścią maszyny."
                    },
                    new Car
                    {
                        Marka = "Bugatti",
                        Model = "Chiron",
                        CenaZaDzien = 3500,
                        Status = StatusSamochodu.Dostepny,
                        Zdjecie = "chiron.jpg",
                        Silnik = "8.0L W16 Quad-Turbo",
                        Spalanie = "22.0L/100km",
                        MocKM = 1500,
                        PrzyspieszenieSek = 2.4m,
                        Naped = "AWD",
                        Opis = "Bugatti Chiron to nie samochód – to wydarzenie. 1500 KM i prędkość maksymalna przekraczająca 400 km/h mówią same za siebie. To inżynieryjne arcydzieło oferuje absolutną dominację na każdej drodze. Luksus i przemoc mechaniczna połączone w jednym, bezkompromisowym aucie."
                    },
                    new Car
                    {
                        Marka = "Mercedes-AMG",
                        Model = "GT Black",
                        CenaZaDzien = 1600,
                        Status = StatusSamochodu.Dostepny,
                        Zdjecie = "merolblack.jpg",
                        Silnik = "4.0L V8 Biturbo",
                        Spalanie = "13.0L/100km",
                        MocKM = 730,
                        PrzyspieszenieSek = 3.2m,
                        Naped = "RWD",
                        Opis = "GT Black Series to torowy potwór od Mercedesa. Z 730 KM i potężnym spojlerem jest gotów zdominować każdy zakręt. Niemiecka precyzja połączona z brutalną mocą tworzy bestię, której nie da się ujarzmić ot tak. Idealny wybór dla tych, którzy chcą poczuć, co znaczy AMG."
                    },
                    new Car
                    {
                        Marka = "Bentley",
                        Model = "Continental GT Speed",
                        CenaZaDzien = 1900,
                        Status = StatusSamochodu.Dostepny,
                        Zdjecie = "bentley.jpg",
                        Silnik = "6.0L W12 Twin-Turbo",
                        Spalanie = "14.0L/100km",
                        MocKM = 659,
                        PrzyspieszenieSek = 3.5m,
                        Naped = "AWD",
                        Opis = "Continental GT Speed to luksusowa rakieta dla dżentelmena. Łączy niezrównany komfort z niesamowitą dynamiką. Silnik W12 oferuje ogromny moment obrotowy dostępny od najniższych obrotów. To samochód, którym podróżuje się z klasą i prędkością jednocześnie."
                    },
                    new Car
                    {
                        Marka = "Rolls-Royce",
                        Model = "Wraith",
                        CenaZaDzien = 2500,
                        Status = StatusSamochodu.Dostepny,
                        Zdjecie = "rolls.jpg",
                        Silnik = "6.6L V12 Twin-Turbo",
                        Spalanie = "15.0L/100km",
                        MocKM = 624,
                        PrzyspieszenieSek = 4.3m,
                        Naped = "RWD",
                        Opis = "Wraith to najbardziej dynamiczny Rolls-Royce w historii marki. Jego 624 KM to cicha potęga ukryta pod maską pełną elegancji. We wnętrzu – pałac na kołach, na zewnątrz – bestia zdolna przyspieszać jak sportowe coupe. Perfekcyjne połączenie luksusu i osiągów najwyższej klasy."
                    }
                );

                SaveChanges();
            }
        }
    }
}

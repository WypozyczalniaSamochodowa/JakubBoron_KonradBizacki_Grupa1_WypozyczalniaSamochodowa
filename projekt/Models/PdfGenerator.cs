using projekt.Models;
using QuestPDF.Fluent;
using QuestPDF.Helpers;
using QuestPDF.Infrastructure;
using System;
using System.IO;

namespace projekt.Utils
{
    public static class PdfGenerator
    {
        public static byte[] GenerujFakture(Klient klient, Car auto, Wypozyczenie wypozyczenie)
        {
            var document = Document.Create(container =>
            {
                container.Page(page =>
                {
                    page.Size(PageSizes.A4);
                    page.Margin(30);
                    page.PageColor(Colors.White);
                    page.DefaultTextStyle(x => x.FontSize(14));

                    page.Content()
                        .Column(column =>
                        {
                            column.Item().Text("Faktura za wypożyczenie samochodu").Bold().FontSize(20).Underline();
                            column.Item().Text($"Data: {DateTime.Now:dd.MM.yyyy}");
                            column.Item().Text("");

                            column.Item().Text($"Klient: {klient.Imie} {klient.Nazwisko}");
                            column.Item().Text($"Email: {klient.Email}");
                            column.Item().Text($"Telefon: {klient.Telefon}");
                            column.Item().Text("");

                            column.Item().Text($"Samochód: {auto.Marka} {auto.Model}");
                            column.Item().Text($"Okres: {wypozyczenie.DataOd:dd.MM.yyyy} - {wypozyczenie.DataDo:dd.MM.yyyy} ({wypozyczenie.Dni} dni)");
                            column.Item().Text($"Cena całkowita: {wypozyczenie.CenaCalkowita} zł");
                        });
                });
            });

            return document.GeneratePdf();
        }
    }
}

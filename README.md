# 🚗 ToFastToRent – Wypożyczalna Samochodów

Projekt ASP.NET Core 8 z Razor Pages do zarządzania wypożyczalnią samochodów. Aplikacja umożliwia przegląd dostępnych pojazdów, filtrowanie, sortowanie i wyszukiwanie modeli oraz dokonywanie rezerwacji. Obsługuje role użytkownika i administratora oraz działa na bazie danych SQLite.

---

## 📑 Spis treści

- [Opis funkcjonalności](#opis-funkcjonalności)
- [Widok Index.cshtml](#widok-indexcshtml)
- [HomeController.cs](#homecontrollercs)
- [Technologie](#technologie)
- [Instalacja](#instalacja)
- [Schemat bazy danych](#schemat-bazy-danych)
- [Możliwości rozwoju](#możliwości-rozwoju)
- [Autorzy](#autorzy)

---

## 🧩 Opis funkcjonalności

| Funkcja                         | Opis |
|----------------------------------|------|
| Wyświetlanie pojazdów           | Lista samochodów z bazy danych |
| Karuzela samochodów             | Lista przesuwająca się w obie strony |
| Obliczanie ceny                 | Automatycznie aktualizująca się cena |
| Szczegóły samochodu             | Link „Szczegóły” |
| Warunkowe linki zależnie od roli | Admin może edytować i usuwać, użytkownik rezerwuje |

---

## 🖼 Widok `Index.cshtml`

Widok `Index.cshtml` zawiera:

- Formularz z filtrowaniem po marce i wyszukiwaniem
- Linki do sortowania (`Cena ⬆`, `Cena ⬇`)
- Warunkowe przyciski `Rezerwuj` lub `Edytuj/Usuń` w zależności od roli
- Dane wyświetlane w formie listy `<div>` z klasą `car-card`

---

## 🧠 `HomeController.cs`

Główna logika kontrolera:

- Pobieranie listy samochodów z `CarService`
- Obsługa filtrowania po marce (`brand`)
- Wyszukiwanie (`searchString`)
- Sortowanie po cenie (`sortOrder`)
- Zwracanie listy do widoku `Index.cshtml`

---

## 🔧 Technologie

| Obszar     | Narzędzia |
|------------|-----------|
| Framework  | ASP.NET Core 8.0 |
| UI         | Razor Pages, HTML, CSS |
| ORM        | Entity Framework Core 8 |
| Baza danych | SQLite |
| Inne       | Dependency Injection, RoleManager, Identity, LINQ |

---

## 🚀 Instalacja

git clone https://github.com/WypozyczalniaSamochodowa/JakubBoron_KonradBizacki_Grupa1_WypozyczalniaSamochodowa
- cd CarRentalApp
- dotnet restore
- dotnet run
## 🗄 Schemat bazy danych

W projekcie wykorzystano prosty model danych oparty na SQLite.

### Tabela: `Cars`

| Kolumna       | Typ           | Opis                              |
|---------------|---------------|-----------------------------------|
| Id            | int (PK)      | Unikalny identyfikator pojazdu   |
| Brand         | string        | Marka pojazdu (np. Ford, BMW)    |
| Model         | string        | Model pojazdu (np. Focus, X5)    |
| PricePerDay   | decimal       | Cena za dzień wynajmu            |
| Available     | bool          | Dostępność pojazdu               |

Przy starcie aplikacji baza danych jest inicjalizowana przez `CarService`.

---

## 🔮 Możliwości rozwoju

- ✅ Moduł rezerwacji z wyborem dat (kalendarz)
- ✅ Historia wynajmów przypisana do użytkownika
- ✅ System powiadomień e-mail lub SMS
- 🔄 Wersja mobilna lub responsywny PWA
- 🔄 Integracja z płatnościami online

---

## 👨‍💻 Autorzy

Projekt wykonany przez:
- Jakub Boroń
- Konrad Bizacki

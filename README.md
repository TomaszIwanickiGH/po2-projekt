# Dokumentacja Aplikacji do Zarządzania Wypożyczeniami Książek

## Opis aplikacji

Aplikacja "Zarządzanie wypożyczeniami książek" umożliwia użytkownikowi zarządzanie bazą danych książek, klientów oraz wypożyczeń. Główne funkcje aplikacji obejmują:

### Dodawanie książek
Pozwala na wprowadzenie nowych książek do systemu z informacjami o tytule, autorze, gatunku oraz dostępności.

### Dodawanie klientów
Umożliwia dodanie nowych klientów, którzy będą mogli wypożyczać książki.

### Zarządzanie wypożyczeniami
Obsługuje proces wypożyczania książek przez klientów, w tym daty wypożyczenia oraz zwrotu.

Aplikacja korzysta z relacyjnej bazy danych PostgreSQL, w której przechowywane są dane dotyczące książek, użytkowników i wypożyczeń.

## Opis bazy danych

Baza danych zawiera trzy główne tabele: `Books`, `Customers`, oraz `Rentals`.

### Tabele w bazie danych:

#### 1. Tabela `Books`
Przechowuje informacje o książkach dostępnych w systemie.

- `BookId` (PK) – Unikalny identyfikator książki  
- `Title` – Tytuł książki  
- `Author` – Autor książki  
- `Genre` – Gatunek książki  
- `IsAvailable` – Status dostępności książki (true/false)

#### 2. Tabela `Customers`
Przechowuje dane o klientach korzystających z aplikacji.

- `CustomerId` (PK) – Unikalny identyfikator klienta  
- `FullName` – Imię i nazwisko klienta  
- `Email` – Adres email klienta  
- `DateOfBirth` – Data urodzin klienta

#### 3. Tabela `Rentals`
Zawiera dane o wypożyczeniach książek przez klientów.

- `RentalId` (PK) – Unikalny identyfikator wypożyczenia  
- `BookId` (FK) – Powiązanie z książką, która została wypożyczona  
- `CustomerId` (FK) – Powiązanie z klientem, który wypożyczył książkę  
- `RentDate` – Data wypożyczenia książki  
- `ReturnDate` – Data zwrotu książki

### Relacje

Tabela `Rentals` ma relacje wiele do jednego z tabelami `Books` oraz `Customers`, co oznacza, że każdy rekord w tabeli wypożyczeń odnosi się do jednej książki oraz jednego klienta.

## Konfiguracja bazy danych

Aplikacja wykorzystuje bazę danych **PostgreSQL 14.0**. W celu uruchomienia bazy danych i konfiguracji połączenia w aplikacji, należy:

### Stworzyć bazę danych w PostgreSQL:

Przykład polecenia SQL do utworzenia bazy:

```sql
CREATE DATABASE BookRental;
```

### Konfiguracja połączenia w aplikacji

Połączenie z bazą danych konfigurowane jest w pliku `appsettings.json`:

```json
{
  "ConnectionStrings": {
    "DefaultConnection": "Host=localhost;Port=5432;Database=BookRental;Username=yourUsername;Password=yourPassword"
  }
}
```

### Zainstalowanie niezbędnych pakietów NuGet

Upewnij się, że zainstalowane są następujące pakiety:

- `Microsoft.EntityFrameworkCore`
- `Microsoft.EntityFrameworkCore.SqlServer`
- `Microsoft.EntityFrameworkCore.Tools`

## Instrukcja uruchomienia aplikacji

### 1. Wymagania wstępne

Aby uruchomić aplikację, musisz mieć zainstalowane:

- .NET SDK (wersja 6.0 lub wyższa)
- PostgreSQL 14.0 lub inna relacyjna baza danych
- Visual Studio (lub inny edytor, który wspiera .NET)

### 2. Uruchomienie aplikacji

Pobierz repozytorium lub sklonuj projekt:

```bash
git clone https://github.com/TomaszIwanickiGH/po2-projekt
```
### 3. Skonfiguruj połączenie z bazą danych w pliku `appsettings.json`

## Walidacja pól formularzy

Formularze w aplikacji zawierają walidację danych przy użyciu atrybutów w modelach. Zostały zastosowane następujące reguły walidacji:

- **Wymagane pola** – każde pole wymagane w formularzu (np. tytuł książki, imię i nazwisko klienta) jest oznaczone atrybutem `[Required]`.
  
- **Długość pola** – w przypadku pól tekstowych, takich jak `FullName` i `Email`, wprowadzono ograniczenia na maksymalną długość.

- **Regular Expression** – pole `Email` walidowane jest przy użyciu wyrażenia regularnego, aby sprawdzić poprawność adresu e-mail.

- **Daty** – pole `RentDate` i `ReturnDate` muszą zawierać poprawną datę.

## Instrukcja obsługi

### Tworzenie bazy danych

1. Stwórz bazę danych `BookRentalDb`.
2. Dokonaj migracji.
3. Wklej skrypt SQL z pliku `baza.txt` do skryptu SQL w pgAdmin.

### Zarządzanie książkami

#### Dodawanie książki:

1. Aby dodać książkę, kliknij przycisk **"Dodaj książkę"**.
2. Wypełnij formularz, podając tytuł, autora, gatunek oraz dostępność książki.
3. Kliknij **"Zapisz"**, aby dodać książkę do systemu.

#### Wyświetlanie książek:

1. Kliknij zakładkę **"Książki"**, aby wyświetlić listę książek.
2. Możesz przeglądać dostępne książki w tabeli.

### Zarządzanie użytkownikami

#### Dodawanie klienta:

1. Aby dodać klienta, kliknij przycisk **"Dodaj klienta"**.
2. Wypełnij formularz, podając imię i nazwisko, email, numer telefonu oraz adres.
3. Kliknij **"Zapisz"**, aby dodać klienta.

#### Wyświetlanie użytkowników:

1. Kliknij zakładkę **"Użytkownicy"**, aby zobaczyć listę klientów.

### Zarządzanie wypożyczeniami

#### Dodawanie wypożyczenia:

1. Aby dodać wypożyczenie, wybierz klienta i książkę z rozwijanych list.
2. Wprowadź datę wypożyczenia oraz datę zwrotu.
3. Kliknij **"Zapisz"**, aby dodać wypożyczenie.

#### Wyświetlanie wypożyczeń:

1. Kliknij zakładkę **"Wypożyczenia"**, aby wyświetlić listę wszystkich wypożyczeń.


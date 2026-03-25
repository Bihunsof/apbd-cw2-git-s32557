# EquipmentRental

Aplikacja konsolowa do obslugi wypozyczenia spretu na uczelni

## Jak urchomic:
```bash
dotnet build
dotnet run --project EquipmentRental/EquipmentRental.csproj
```

### Krotki opis decyzji projektowych:
- Program.cs tylko uruchamia aplikacje
- UI/ tylko scenariusz demonstracyjny
- Model domenowy jest oddzielony od logiki biznesowej
- Services/ tylko logika biznesowa
- Common/ elementy wspoldzielone
- kazda klasa ma swoje jedno glowne zadanie
- Klase domenowe nie zaleza od konsoli

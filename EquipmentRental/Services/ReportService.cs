namespace EquipmentRental.Services;
using EquipmentRental.Domain;

public class ReportService
{
    private readonly RentalService _rentalService;
    
    public ReportService(RentalService rentalService)
    {
        _rentalService = rentalService;
    }

    public string BuildSummary(DateTime referenceDate)
    {
        var totalEquipment = _rentalService.EquipmentItems.Count;
        var available = _rentalService.EquipmentItems.Count(e => e.Status == EquipmentStatus.Available);
        var rented = _rentalService.EquipmentItems.Count(e => e.Status == EquipmentStatus.Rented);
        var unavailable = _rentalService.EquipmentItems.Count(e => e.Status == EquipmentStatus.Unavailable);
        var activeRentals = _rentalService.Rentals.Count(r => r.IsActive);
        var overdueRentals = _rentalService.GetOverdueRentals(referenceDate).Count();
        var totalPenalty = _rentalService.Rentals.Sum(r => r.Penalty);

        return $@"===== RAPORT WYPOŻYCZALNI =====
Data raportu: {referenceDate:yyyy-MM-dd}
Sprzet lacznie: {totalEquipment}
Dostepny: {available}
Wypozyczony: {rented}
Niedostepny: {unavailable}
Aktywne wypozyczenia: {activeRentals}
Przeterminowane wypozyczenia: {overdueRentals}
Naliczone kary lacznie: {totalPenalty} PLN
==============================";
    }
}
namespace EquipmentRental.Services;
using EquipmentRental.Domain;

public class RentalPolicy
{
    public decimal PenaltyPerDay { get; } = 15m;
    
    public bool CanBorrow(User user, int activeRentalsCount) =>
    activeRentalsCount < user.MaxActiveRentals;

    public decimal CalculatePenalty(DateTime dueDate, DateTime returnDate)
    {
        if (returnDate.Date <= dueDate.Date)
        {
            return 0m;
        }

        var daysLate = (returnDate.Date - dueDate.Date).Days;
        return daysLate * PenaltyPerDay;
    }
}
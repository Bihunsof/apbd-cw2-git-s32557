namespace EquipmentRental.Services;
using EquipmentRental.Domain;


public class RentalService
{
    private readonly List<User> _users = new();
    private readonly List<Equipment> _equipmentItems = new();
    private readonly List<Rental> _rentals = new();
    private readonly RentalPolicy _policy;
    
    public RentalService(RentalPolicy policy)
    {
        _policy = policy;
    }
    
    public IReadOnlyList<User> Users => _users.AsReadOnly();
    public IReadOnlyList<Equipment> EquipmentItems => _equipmentItems.AsReadOnly();
    public IReadOnlyList<Rental> Rentals => _rentals.AsReadOnly();
    
    public void AddUser(User user) => _users.Add(user);
    public void AddEquipment(Equipment equipment) => _equipmentItems.Add(equipment);
    
    public IEnumerable<Equipment> GetAllEquipment() => _equipmentItems;
    
    public IEnumerable<Equipment> GetAvailableEquipment() => _equipmentItems.Where(e => e.Status == EquipmentStatus.Available);

    private User GetUserOrThrow(string userId) =>
        _users.FirstOrDefault(u => u.Id == userId)
        ?? throw new InvalidOperationException($"Nie znaleziono uzytkownika: {userId}.");

    private Equipment GetEquipmentOrThrow(string equipmentId) =>
        _equipmentItems.FirstOrDefault(e => e.Id == equipmentId)
        ?? throw new InvalidOperationException($"Nie znaleziono sprzetu {equipmentId}.");

    public Rental RentEquipment(string userId, string equipmentId, int durationDays, DateTime? rentedAt = null)
    {
        var user = GetUserOrThrow(userId);
        var equipment = GetEquipmentOrThrow(equipmentId);

        if (equipment.Status != EquipmentStatus.Available)
        {
            throw new InvalidOperationException($"Sprzet {equipment.Id} nie jest dostepny.");
        }

        var activeRentalsCount = _rentals.Count(r => r.User.Id == userId && r.IsActive);

        if (!_policy.CanBorrow(user, activeRentalsCount))
        {
            throw new InvalidOperationException($"Uzytkownik {user.Id} przekroczyl limit wypoяyczen.");
        }

        var startDate = rentedAt ?? DateTime.Now;
        var rental = new Rental(user, equipment, startDate, durationDays);

        equipment.MarkAsRented();
        _rentals.Add(rental);

        return rental;
    }

    public decimal ReturnEquipment(string rentalId, DateTime? returnedAt = null)
    {
        var rental = _rentals.FirstOrDefault(r => r.Id == rentalId && r.IsActive)
                     ?? throw new InvalidOperationException($"Nie znaleziono aktywnego wypozyczenia {rentalId}.");

        var actualReturnDate = returnedAt ?? DateTime.Now;
        var penalty = _policy.CalculatePenalty(rental.DueDate, actualReturnDate);

        rental.MarkReturned(actualReturnDate, penalty);
        rental.Equipment.MarkAsAvailable();

        return penalty;
    }

    public void MarkEquipmentUnavailable(string equipmentId)
    {
        var equipment = GetEquipmentOrThrow(equipmentId);

        if (equipment.Status == EquipmentStatus.Rented)
        {
            throw new InvalidOperationException("Nie mozna oznaczyc jako niedostępnego sprzetu, ktory jest aktualnie wypozyczony.");
        }

        equipment.MarkAsUnavailable();
    }

    public IEnumerable<Rental> GetActiveRentalsForUser(string userId) =>
        _rentals.Where(r => r.User.Id == userId && r.IsActive);

    public IEnumerable<Rental> GetOverdueRentals(DateTime referenceDate) =>
        _rentals.Where(r => r.IsActive && r.DueDate.Date < referenceDate.Date);
}
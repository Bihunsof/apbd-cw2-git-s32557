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
}
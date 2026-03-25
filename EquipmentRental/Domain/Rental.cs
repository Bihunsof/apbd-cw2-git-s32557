namespace EquipmentRental.Domain;
using EquipmentRental.Common;

public class Rental
{
    public Rental(User user, Equipment equipment, DateTime rentedAt, int durationDays)
    {
        Id = IdGenerator.NextRentalId();
        User = user;
        Equipment =  equipment;
        RentedAt =  rentedAt;
        DueDate = rentedAt.AddDays(durationDays);
    }
    
    public string Id { get; }
    public User User { get; }
    public Equipment Equipment { get; }
    public DateTime RentedAt { get; }
    public DateTime DueDate { get; }
    public DateTime? ReturnedAt { get; private set; }
    public decimal Penalty { get; private set; }

    public bool IsActive => ReturnedAt is null;
    public bool IsReturnedLate => ReturnedAt.HasValue && ReturnedAt.Value.Date > DueDate.Date;

    public void MarkReturned(DateTime returnedAt, decimal penalty)
    {
        ReturnedAt = returnedAt;
        Penalty = penalty;
    }

    public override string ToString()
    {
        var status = IsActive ? "Aktywne" : $"Zwrocone ({ReturnedAt:yyyy-MM-dd})";
        return $"{Id} | {Equipment.Name} ({Equipment.Id}) | {User.FirstName} {User.LastName} | " +
               $"od {RentedAt:yyyy-MM-dd} do {DueDate:yyyy-MM-dd} | {status} | kara: {Penalty} PLN";
    }
}
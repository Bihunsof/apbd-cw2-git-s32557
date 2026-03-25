namespace EquipmentRental.Domain;
using EquipmentRental.Common;

public abstract class User
{
    protected User(string firstName, string lastName)
    {
        Id = IdGenerator.NextUserId();
        FirstName = firstName;
        LastName = lastName;
    }
    
    public string Id { get; }
    public string FirstName { get; }
    public string LastName { get; }
    
    public abstract UserType Type { get; }
    public abstract int MaxActiveRentals { get; }
    
    public override string ToString() => $"{Id} | {FirstName} {LastName} | {Type} | Limit: {MaxActiveRentals}";
}
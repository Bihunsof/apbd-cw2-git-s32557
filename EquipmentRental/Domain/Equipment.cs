using EquipmentRental.Common;
namespace EquipmentRental.Domain;

public abstract class Equipment
{

    protected Equipment(string name)
    {
        Id = IdGenerator.NextEquipmentId();
        Name = name;
        Status = EquipmentStatus.Available;
    }
    
    public string Id { get; }
    public string Name { get; }
    public EquipmentStatus Status { get; private set; }
    
    public void MarkAsRented() => Status =  EquipmentStatus.Rented;
    public void MarkAsAvailable() => Status =  EquipmentStatus.Available;
    public void MarkAsUnavailable() => Status = EquipmentStatus.Unavailable;

    public abstract string GetDetails();
    
    public override string ToString() => $"{Id} | {Name}  | {Status} | {GetDetails()}";
}
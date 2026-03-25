namespace EquipmentRental.Domain;

public class Employee : User
{
    public Employee(string firstName, string lastName) : base(firstName, lastName)
    {
        
    }
    
    public override UserType Type => UserType.Employee;
    public override int MaxActiveRentals => 5;
}
namespace EquipmentRental.Domain;

public class Student : User
{
public Student(string firstName, string lastName) : base(firstName, lastName)
{
    
}

public override UserType Type => UserType.Student;
public override int MaxActiveRentals => 2;
}
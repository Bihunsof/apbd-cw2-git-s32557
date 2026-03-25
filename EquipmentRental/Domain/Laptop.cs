namespace EquipmentRental.Domain;

public class Laptop : Equipment
{
    public Laptop(string name, int ramGb, string processor) : base(name)
    {
        RamGb =  ramGb;
        Processor =  processor;
    }
    
    public int RamGb { get; }
    public string Processor { get; }
    
    public override string GetDetails() => $"Laptop | Ram: {RamGb} GB | Processor: {Processor}";
}
namespace EquipmentRental.Domain;

public class Projector : Equipment
{
    public Projector(string name, int brightnessLumens, string resolution) : base(name)
    {
        BrightnessLumens = brightnessLumens;
        Resolution =  resolution;
    }
    
    public string Resolution { get; }
    public int BrightnessLumens { get; }
    
    public override string GetDetails() => $"Projector | Jasnosc: {BrightnessLumens} lm | Resolution: {Resolution}";
}
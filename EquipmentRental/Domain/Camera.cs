namespace EquipmentRental.Domain;

public class Camera : Equipment
{
    public Camera(string name, int megapixels, string lensType) : base(name)
    {
        Megapixels =  megapixels;
        LensType =  lensType;
    }
    public string LensType { get; }
    public int Megapixels { get; }

    public override string GetDetails() => $"Camera | MP: {Megapixels} | Lens Type: {LensType}";
}
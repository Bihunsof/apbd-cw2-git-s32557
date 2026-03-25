namespace EquipmentRental.Common;

public static class IdGenerator
{
    private static int _equipmentId = 1;
    private static int _userId = 1;
    private static int _rentalId = 1;
    
    public static string NextEquipmentId() => $"EQ-{_equipmentId++:D3}";
    public static string NextUserId() => $"USR-{_userId++:D3}";
    public static string NextRentalId() => $"RNT-{_rentalId++:D3}";
}
namespace EquipmentRental.UI;
using EquipmentRental.Domain;
using EquipmentRental.Services;

public class DemoScenario
{
    private readonly RentalService _rentalService;
    private readonly ReportService _reportService;

    public DemoScenario(RentalService rentalService, ReportService reportService)
    {
        _rentalService = rentalService;
        _reportService = reportService;
    }

    public void Run()
    {
        var baseDate = new DateTime(2026, 3, 1);

        var student1 = new Student("Jan", "Kowalski");
        var student2 = new Student("Anna", "Nowak");
        var employee1 = new Employee("Michal", "Tomaszewski");
        
        _rentalService.AddUser(student1);
        _rentalService.AddUser(student2);
        _rentalService.AddUser(employee1);

        var laptop1 = new Laptop("Macbook", 16, "Intel i7");
        var laptop2 = new Laptop("Macbook Pro", 46, "Apple silicon M4");
        var projector1 = new Projector("Jakis", 3000, "AMD Ryzen 7");
        var camera1 = new Camera("Canon", 24, "18-55mm");
        var camera2 = new Camera("Sony", 36, "15-50mm");
        
        _rentalService.AddEquipment(laptop1);
        _rentalService.AddEquipment(laptop2);
        _rentalService.AddEquipment(projector1);
        _rentalService.AddEquipment(camera1);
        _rentalService.AddEquipment(camera2);

        PrintSection("UZYTKOWNICY");
        foreach (var user in _rentalService.Users)
        {
            Console.WriteLine(user);
        }

        PrintSection("POPRAWNE WYPOZYCZENIE");
        var rental1 = _rentalService.RentEquipment(student1.Id, laptop1.Id,7, baseDate);

        PrintSection("PROBA WYPOZYCZENIA NIEDOSTEPNEGO SPRZETU");
        TryAction(() => _rentalService.RentEquipment(employee1.Id, laptop1.Id, 3, baseDate.AddDays(1)));
        
        PrintSection("PRZEKROCZENIE LIMITU STUDENTA");
        _rentalService.RentEquipment(student1.Id, camera1.Id, 5, baseDate);
        TryAction(() => _rentalService.RentEquipment(student1.Id, projector1.Id, 2, baseDate));

        PrintSection("ZWROT W TERMINIE");
        var rental2 = _rentalService.RentEquipment(employee1.Id, projector1.Id, 3, baseDate.AddDays(1));
        var onTimePenalty = _rentalService.ReturnEquipment(rental2.Id, baseDate.AddDays(3));
        Console.WriteLine($"Kara za terminowy zwrot: {onTimePenalty} PLN");

        PrintSection("ZWROT OPOZNIONY");
        var rental3 = _rentalService.RentEquipment(employee1.Id, laptop2.Id, 3, baseDate);
        var latePenalty = _rentalService.ReturnEquipment(rental3.Id, baseDate.AddDays(10));
        Console.WriteLine($"Kara za spóźniony zwrot: {latePenalty} PLN");

        PrintSection("OZNACZENIE SPRZETU JAKO NIEDOSTEPNY");
        _rentalService.MarkEquipmentUnavailable(camera2.Id);
        Console.WriteLine($"{camera2.Id} oznaczono jako niedostępny.");

        PrintSection("AKTYWNE WYPOZYCZENIA STUDENTA");
        foreach (var rental in _rentalService.GetActiveRentalsForUser(student1.Id))
        {
            Console.WriteLine(rental);
        }

        var reportDate = baseDate.AddDays(14);

        PrintSection("PRZETERMINOWANE WYPOZYCZENIA");
        foreach (var rental in _rentalService.GetOverdueRentals(reportDate))
        {
            Console.WriteLine(rental);
        }

        PrintSection("KONCOWY RAPORT");
        Console.WriteLine(_reportService.BuildSummary(reportDate));
    }

    private static void TryAction(Action action)
    {
        try
        {
            action();
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Operacja odrzucona: {ex.Message}");
        }
    }

    private static void PrintSection(string title)
    {
        Console.WriteLine();
        Console.WriteLine($"===== {title} =====");
    }
}

namespace Drivers.Domain.Models;

public class DriversSearchOptions
{
    public int Skip { get; set; }
    public int Take { get; set; } = 10;
    public string? Location { get; set; }
}


namespace Drivers.Domain.Models;

public class Driver
{
    public Guid Id { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Location { get; set; }
}

namespace Drivers.Domain.Models;

/// <summary>
/// Driver
/// </summary>
public class Driver
{
    /// <summary>
    /// Drivers unique ID
    /// </summary>
    public Guid Id { get; set; }
    /// <summary>
    /// Driver's first name
    /// </summary>
    public string FirstName { get; set; }
    /// <summary>
    /// Driver's last name
    /// </summary>
    public string LastName { get; set; }
    /// <summary>
    /// Driver's current location
    /// </summary>
    public string Location { get; set; }
}

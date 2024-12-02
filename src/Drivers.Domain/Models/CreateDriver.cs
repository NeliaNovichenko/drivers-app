namespace Drivers.Domain.Models;

/// <summary>
/// Payload for creating a new driver
/// </summary>
public class CreateDriver
{
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


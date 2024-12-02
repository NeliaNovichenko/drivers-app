namespace Drivers.Domain.Models;

/// <summary>
/// Options for searching drivers
/// </summary>
public class DriversSearchOptions
{
    /// <summary>
    /// The number of records to skip in the search results for pagination.
    /// </summary>
    public int Skip { get; set; }

    /// <summary>
    /// The maximum number of records to retrieve in the search results. 
    /// Defaults to 10 if not specified.
    /// </summary>
    public int Take { get; set; } = 10;

    /// <summary>
    /// The location filter for searching drivers. 
    /// If null or empty, no location filter is applied.
    /// </summary>
    public string? Location { get; set; }
}


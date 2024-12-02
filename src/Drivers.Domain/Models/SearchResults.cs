namespace Drivers.Domain.Models;

/// <summary>
/// A paginated search result containing a collection of items and the total count.
/// </summary>
/// <typeparam name="T">The type of items in the search results.</typeparam>
public class SearchResults<T>
{
    /// <summary>
    /// The total number of items that match the search criteria.
    /// </summary>
    public int TotalCount { get; set; }

    /// <summary>
    /// The collection of items returned for the current page of results.
    /// </summary>
    public T[] Items { get; set; } = Array.Empty<T>();
}

namespace Drivers.Domain.Models;

public class SearchResults<T>
{
    public int TotalCount { get; set; }
    public T[] Items { get; set; } = [];
}

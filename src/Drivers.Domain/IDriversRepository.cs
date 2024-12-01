using Drivers.Domain.Models;

namespace Drivers.Domain;

public interface IDriversRepository
{
    Task<Driver> Create(Driver driver);
    Task<SearchResults<Driver>> Get(DriversSearchOptions searchOptions);
}

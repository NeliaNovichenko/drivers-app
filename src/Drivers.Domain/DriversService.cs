using Drivers.Domain.Models;

namespace Drivers.Domain;

public interface IDriversService
{
    Task<Driver> Create(CreateDriver createDriver);
    Task<SearchResults<Driver>> Get(DriversSearchOptions searchOptions);
}

public class DriversService : IDriversService
{
    private readonly IDriversRepository _driversRepository;

    public DriversService(IDriversRepository driversRepository)
    {
        _driversRepository = driversRepository;
    }

    public async Task<Driver> Create(CreateDriver createDriver)
    {
        var driver = new Driver { Id = Guid.NewGuid(), FirstName = createDriver.FirstName, LastName = createDriver.LastName, Location = createDriver.Location };
        return await _driversRepository.Create(driver);
    }

    public Task<SearchResults<Driver>> Get(DriversSearchOptions searchOptions)
    {
        return _driversRepository.Get(searchOptions);
    }
}
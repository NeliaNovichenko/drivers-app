using Drivers.Domain;
using Drivers.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace Drivers.Db;

public class DriversRepository : IDriversRepository
{
    private readonly DriversDBContext _dBContext;

    public DriversRepository(DriversDBContext dBContext)
    {
        _dBContext = dBContext;
    }

    public async Task<Driver> Create(Driver driver)
    {
        await _dBContext.Drivers.AddAsync(driver);
        await _dBContext.SaveChangesAsync();

        return driver;
    }

    public async Task<SearchResults<Driver>> Get(DriversSearchOptions searchOptions)
    {
        var query = _dBContext.Drivers.AsQueryable();

        if (!string.IsNullOrWhiteSpace(searchOptions.Location)) 
            query = query.Where(d => d.Location == searchOptions.Location);

        var count = await query.CountAsync();
        var items = await query.Skip(searchOptions.Skip).Take(searchOptions.Take).ToArrayAsync();

        return new SearchResults<Driver>
        {
            Items = items,
            TotalCount = count
        };
    }
}
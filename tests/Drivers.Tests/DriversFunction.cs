using AutoBogus;
using Drivers.Domain.Models;
using FluentAssertions;
using Newtonsoft.Json;
using Xunit;

namespace Drivers.UnitTests;

public class DriversFunctionTests : IClassFixture<TestApplicationFactory>
{
    private readonly TestApplicationFactory _testApplicationFactory;

    public DriversFunctionTests(TestApplicationFactory testApplicationFactory)
    {
        _testApplicationFactory = testApplicationFactory;
    }

    [Fact]
    public async Task CreateDriver_ShouldReturn_201_StatusCode()
    {
        var driver = new AutoFaker<CreateDriver>().Generate();
        var response = await _testApplicationFactory.FunctionsHttpClient.PostAsJsonAsync("api/drivers", driver);

        response.StatusCode.Should().Be(System.Net.HttpStatusCode.Created);
    }

    [Fact]
    public async Task CreateDriver_ShouldReturn_CreatedDriver()
    {
        var driver = new AutoFaker<CreateDriver>().Generate();
        var response = await _testApplicationFactory.FunctionsHttpClient.PostAsJsonAsync("api/drivers", driver);

        string responseBody = await response.Content.ReadAsStringAsync();
        var createdDriver = JsonConvert.DeserializeObject<Driver>(responseBody);

        createdDriver.Should().BeEquivalentTo(driver);
    }

    [Fact]
    public async Task CreateDriver_ShouldReturn_GeneratedId()
    {
        var driver = new AutoFaker<CreateDriver>().Generate();
        var response = await _testApplicationFactory.FunctionsHttpClient.PostAsJsonAsync("api/drivers", driver);

        string responseBody = await response.Content.ReadAsStringAsync();
        var createdDriver = JsonConvert.DeserializeObject<Driver>(responseBody);

        createdDriver.Id.Should().NotBeEmpty();
    }

    [Fact]
    public async Task GetDrivers_NoOptions_ShouldReturn_10_Drivers()
    {
        var drivers = new AutoFaker<CreateDriver>().Generate(11);
        await SetUpDrivers(drivers);

        var response = await _testApplicationFactory.FunctionsHttpClient.GetAsync("api/drivers");

        response.StatusCode.Should().Be(System.Net.HttpStatusCode.OK);

        string responseBody = await response.Content.ReadAsStringAsync();
        var driversSearchResult = JsonConvert.DeserializeObject<SearchResults<Driver>>(responseBody);

        driversSearchResult.TotalCount.Should().BeGreaterThanOrEqualTo(11);
        driversSearchResult.Items.Count().Should().Be(10);
    }

    [Fact]
    public async Task GetDrivers_FilterByLocation_ShouldReturn_FilteredResult()
    {
        var drivers = new AutoFaker<CreateDriver>().Generate(10);
        await SetUpDrivers(drivers);

        var response = await _testApplicationFactory.FunctionsHttpClient.GetAsync($"api/drivers?location={drivers[0].Location}");
        string responseBody = await response.Content.ReadAsStringAsync();
        var driversSearchResult = JsonConvert.DeserializeObject<SearchResults<Driver>>(responseBody);

        var filtered = driversSearchResult.Items.Where(d => d.Location == drivers[0].Location);
        driversSearchResult.Items.Count().Should().Be(filtered.Count());
        driversSearchResult.Items.Should().BeEquivalentTo(filtered);
    }

    public async Task SetUpDrivers(List<CreateDriver> drivers)
    {
        foreach(var driver in drivers) 
            await _testApplicationFactory.FunctionsHttpClient.PostAsJsonAsync("api/drivers", driver);
    }
}

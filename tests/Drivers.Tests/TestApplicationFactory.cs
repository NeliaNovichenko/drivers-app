using DotNet.Testcontainers.Builders;
using DotNet.Testcontainers.Containers;
using DotNet.Testcontainers.Images;
using DotNet.Testcontainers.Networks;
using Drivers.Db;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Testcontainers.PostgreSql;
using Xunit;

namespace Drivers.UnitTests;

public class TestApplicationFactory : WebApplicationFactory<Program>, IAsyncLifetime
{
    private readonly INetwork network = new NetworkBuilder().WithName("drivers-int-test-network").Build();
    private readonly IFutureDockerImage _azureFunctionsDockerImage = new ImageFromDockerfileBuilder()
        .WithDockerfileDirectory(CommonDirectoryPath.GetSolutionDirectory(), "")
        .WithDockerfile("AzureFunctions-Testcontainers.Dockerfile")
        .WithBuildArgument("RESOURCE_REAPER_SESSION_ID", ResourceReaper.DefaultSessionId.ToString("D"))
        .Build();

    private IContainer _azureFunctionsContainer;
    private PostgreSqlContainer _postgreSqlContainer;

    public HttpClient FunctionsHttpClient { get; private set; }

    public async Task InitializeAsync()
    {
        // create network for communication between containers
        await network.CreateAsync();

        _postgreSqlContainer = CreatePostgresContainer(network);
        await _postgreSqlContainer.StartAsync();

        // apply migrations
        var context = new DriversDBContextFactory().CreateDbContext(_postgreSqlContainer.GetConnectionString());
        await context.Database.MigrateAsync();

        _azureFunctionsContainer = CreateAzureFunctionsContainer(network);
        await _azureFunctionsDockerImage.CreateAsync();
        await _azureFunctionsContainer.StartAsync();

        FunctionsHttpClient = new HttpClient();
        FunctionsHttpClient.BaseAddress = new UriBuilder(
            Uri.UriSchemeHttp,
            _azureFunctionsContainer.Hostname,
            _azureFunctionsContainer.GetMappedPublicPort(80)
        ).Uri;
    }

    async Task IAsyncLifetime.DisposeAsync()
    {
        await _azureFunctionsContainer.DisposeAsync();
        await _azureFunctionsDockerImage.DisposeAsync();

        await _postgreSqlContainer.DisposeAsync();

        await network.DeleteAsync();
    }

    private PostgreSqlContainer CreatePostgresContainer(INetwork network)
    {
        return new PostgreSqlBuilder()
           .WithImage("postgres:16").WithName("drivers-int-test-db")
           .WithDatabase("drivers-db").WithUsername("testuser").WithPassword("testpassword")
           .WithNetwork(network.Name).WithNetworkAliases(network.Name)
           .Build();
    }

    private IContainer CreateAzureFunctionsContainer(INetwork network)
    {
        return new ContainerBuilder()
           .WithImage(_azureFunctionsDockerImage)
           .WithName("drivers-int-test-functions")
           .WithEnvironment("ConnectionStrings:DriversDb", $"Host={network.Name};Database=drivers-db;Port=5432;Username=testuser;Password=testpassword")
           .WithNetwork(network.Name)
           .WithPortBinding(80, true)
           .WithWaitStrategy(Wait.ForUnixContainer().UntilHttpRequestIsSucceeded(r => r.ForPort(80)))
           .Build();
    }
}

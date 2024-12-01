using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Http;
using Microsoft.Azure.WebJobs.Extensions.OpenApi.Core.Attributes;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using System.Net;
using Drivers.Domain;
using Drivers.Domain.Models;
using System.Collections.Specialized;

namespace Drivers.Api.Functions;

public class DriversFunction
{
    private readonly IDriversService _driversService;
    private readonly ILogger<DriversFunction> _logger;

    public DriversFunction(IDriversService _driversService, ILogger<DriversFunction> logger)
    {
        this._driversService = _driversService;
        _logger = logger;
    }

    [Function("GetDrivers")]
    [OpenApiOperation(operationId: "GetDrivers")]
    [OpenApiParameter(name: nameof(DriversSearchOptions.Location), In = ParameterLocation.Query, Required = false, Type = typeof(string), Description = "The location of the drivers.")]
    [OpenApiParameter(name: nameof(DriversSearchOptions.Skip), In = ParameterLocation.Query, Required = false, Type = typeof(int), Description = "")]
    [OpenApiParameter(name: nameof(DriversSearchOptions.Take), In = ParameterLocation.Query, Required = false, Type = typeof(int), Description = "")]
    [OpenApiResponseWithBody(statusCode: HttpStatusCode.OK, contentType: "application/json", bodyType: typeof(SearchResults<Driver>), Description = "Results of search.")]
    public async Task<IActionResult> GetDrivers(
        [HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = "drivers")] HttpRequestData request)
    {
        _logger.LogInformation("Executing GetDrivers function.");

        var searchOptions = ToSearchOptions(request.Query);
        var result = await _driversService.Get(searchOptions);

        return new OkObjectResult(result);
    }

    [Function("CreateDriver")]
    [OpenApiOperation(operationId: "CreateDriver")]
    [OpenApiRequestBody("application/json", typeof(CreateDriver), Description = "")]
    [OpenApiResponseWithBody(statusCode: HttpStatusCode.OK, contentType: "application/json", bodyType: typeof(Driver), Description = "Created driver.")]
    public async Task<IActionResult> CreateDriver(
        [HttpTrigger(AuthorizationLevel.Anonymous, "post", Route = "drivers")] HttpRequestData request)
    {
        _logger.LogInformation("Executing CreateDriver function.");

        var requestBody = await request.ReadFromJsonAsync<CreateDriver>();
        var result = await _driversService.Create(requestBody);

        return new CreatedResult("", result);
    }

    private DriversSearchOptions ToSearchOptions(NameValueCollection queryParams)
    {
        var searchOptions = new DriversSearchOptions()
        {
            Location = queryParams.Get(nameof(DriversSearchOptions.Location))
        };

        if (int.TryParse(queryParams.Get(nameof(DriversSearchOptions.Skip)), out var skip))
            searchOptions.Skip = skip;

        if (int.TryParse(queryParams.Get(nameof(DriversSearchOptions.Take)), out var take))
            searchOptions.Take = take;

        return searchOptions;
    }
}

using System.Net;
using System.Text.Json;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Http;
using Microsoft.Extensions.Logging;

namespace Api.Function;

public class GetResumeCounter
{
    private readonly ILogger<GetResumeCounter> _logger;
    private readonly IVisitorCounterService _visitorCounterService;

    public GetResumeCounter(ILogger<GetResumeCounter> logger, IVisitorCounterService visitorCounterService)
    {
        _logger = logger;
        _visitorCounterService = visitorCounterService;
    }

    [Function("GetResumeCounter")]
    public async Task<UpdatedCounter> Run([HttpTrigger(AuthorizationLevel.Anonymous, "get", "post")] HttpRequestData req,
    [CosmosDBInput("TokaDB","CloudResume", Connection = "AzureResumeConnectionString", Id = "1", PartitionKey = "1")] Counter counter)
    {
        // Start logging function execution
        _logger.LogInformation("Function 'GetResumeCounter' started.");

        if (counter == null)
        {
            _logger.LogWarning("Counter data not found for id 'id'. Returning a 404 response.");
            var notFoundResponse = req.CreateResponse(HttpStatusCode.NotFound);
            await notFoundResponse.WriteStringAsync("Counter data not found.");
            return new UpdatedCounter
            {
                NewCounter = null,
                HttpResponse = notFoundResponse
            };
        }

        _logger.LogInformation("Counter data retrieved successfully. Current count: {currentCount}", counter.Count);

        // Increment the counter
        _logger.LogInformation("Incrementing counter.");
        counter = _visitorCounterService.IncrementCounter(counter);
        _logger.LogInformation("Counter incremented. New count: {newCount}", counter.Count);

        // Prepare the response
        var response = req.CreateResponse(HttpStatusCode.OK);
        response.Headers.Add("Content-Type", "application/json; charset=utf-8");
        string jsonString = JsonSerializer.Serialize(counter);

        _logger.LogInformation("Writing response with updated counter value.");
        await response.WriteStringAsync(jsonString);

        // Log the final step before returning
        _logger.LogInformation("Response generated and returning the updated counter.");

        return new UpdatedCounter
        {
            NewCounter = counter,
            HttpResponse = response
        };
    }
}

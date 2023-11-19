using System.Net;
using Microsoft.Extensions.Http.Resilience;
using Polly;
using Polly.CircuitBreaker;
using Polly.Fallback;
using Polly.Retry;
using PopularLibraries.PollyExample.Policies;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


builder.Services
    .AddHttpClient("PollyExample", client =>
    {
        client.BaseAddress = new Uri("http://localhost:5005");
    }).AddStandardResilienceHandler()
    .Configure(options =>
    {
        options.Retry.MaxRetryAttempts = 2;
        options.Retry.BackoffType = DelayBackoffType.Exponential;
        options.Retry.Delay = TimeSpan.FromSeconds(500);

        options.CircuitBreaker.SamplingDuration = TimeSpan.FromSeconds(5);
        options.CircuitBreaker.FailureRatio = 0.9;
        options.CircuitBreaker.MinimumThroughput = 5;
        options.CircuitBreaker.BreakDuration = TimeSpan.FromSeconds(10);
    });


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();


//This times is here just to fail the first call and be able to test polly
int times = 0;
app.MapGet("/best-endpoint", () =>
    {
        switch (times)
        {
            case 0:
                times++;
                throw new Exception("just an example");
            case 1:
                times++;
                Thread.Sleep(10000);
                throw new Exception("just an example");
            default:
                return "Ok";
        }
    })
    .WithName("BestEndpoint")
    .WithOpenApi();

app.MapGet("/polly-execution-retry", (IHttpClientFactory httpClientFactory) =>
    {
        HttpClient client = httpClientFactory.CreateClient("PollyExample");

        AsyncRetryPolicy policy = Policy
            .Handle<Exception>()
            .WaitAndRetryAsync(3, retryTime =>
                TimeSpan.FromSeconds(0.5 * retryTime));

        return policy.ExecuteAsync(async () => await client.GetStringAsync("best-endpoint"));
    })
    .WithName("PollyExampleRetry")
    .WithOpenApi();


AsyncCircuitBreakerPolicy policyCircuitBreaker = Policy
    .Handle<Exception>()
    .CircuitBreakerAsync(2, TimeSpan.FromMinutes(10));

app.MapGet("/polly-execution-circuitbreaker", (IHttpClientFactory httpClientFactory) =>
    {
        HttpClient client = httpClientFactory.CreateClient("PollyExample");
        return policyCircuitBreaker.ExecuteAsync(async () => await client.GetStringAsync("best-endpoint"));
    })
    .WithName("PollyExampleCircuitBreaker")
    .WithOpenApi();


app.MapGet("/polly-execution-fallback", (IHttpClientFactory httpClientFactory) =>
    {
        HttpClient client = httpClientFactory.CreateClient("PollyExample");
        AsyncFallbackPolicy<string> fallback = Policy<string>
            .Handle<Exception>()
            .FallbackAsync("All good here, nothing to see");

        return fallback.ExecuteAsync(async () => await client.GetStringAsync("best-endpoint"));
    })
    .WithName("PollyExampleFallback")
    .WithOpenApi();

app.MapGet("/polly-execution-resilience-pipeline", async (IHttpClientFactory httpClientFactory) =>
    {
        HttpClient client = httpClientFactory.CreateClient("PollyExample");

        ResiliencePipeline<HttpResponseMessage> pipeline = new ResiliencePipelineBuilder<HttpResponseMessage>()
            .AddRetry(new RetryStrategyOptions<HttpResponseMessage>()
            {
                ShouldHandle = new PredicateBuilder<HttpResponseMessage>()
                    .Handle<Exception>()
                    .HandleResult(response => response.StatusCode >= HttpStatusCode.InternalServerError),
                MaxRetryAttempts = 3,
                BackoffType = DelayBackoffType.Exponential
            })
            .AddTimeout(TimeSpan.FromSeconds(3))
            .Build();

       await pipeline.ExecuteAsync(async token => await client.GetAsync("best-endpoint"));
    })
    .WithName("PollyExampleResilience")
    .WithOpenApi();

app.Run();
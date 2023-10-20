using PopularLibraries.BackgroundWorkers.UseCases;

namespace PopularLibraries.BackgroundWorkers.Workers;

public class NativeWorker : BackgroundService
{
    private readonly IServiceProvider _serviceProvider;

    public NativeWorker(IServiceProvider serviceProvider)
    {
        _serviceProvider = serviceProvider;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        while (!stoppingToken.IsCancellationRequested)
        {
            using (IServiceScope scope = _serviceProvider.CreateScope())
            {
                ISampleUseCase sampleUseCase = scope.ServiceProvider
                    .GetRequiredService<ISampleUseCase>();

                sampleUseCase.Execute();
            }
            
            await Task.Delay(1000, stoppingToken);
        }
    }
}
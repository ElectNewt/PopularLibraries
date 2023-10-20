using PopularLibraries.BackgroundWorkers.UseCases;
using Quartz;

namespace PopularLibraries.BackgroundWorkers.Workers;

public class QuartzExampleJob : IJob
{
    private readonly ISampleUseCase _sampleUseCase;

    public QuartzExampleJob(ISampleUseCase sampleUseCase)
    {
        _sampleUseCase = sampleUseCase;
    }

    public Task Execute(IJobExecutionContext context)
    {
        _sampleUseCase.Execute();
        return Task.CompletedTask;
    }
}
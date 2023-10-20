namespace PopularLibraries.BackgroundWorkers.UseCases;

public interface ISampleUseCase
{
    void Execute();
}

public class SampleUseCase : ISampleUseCase
{
    public void Execute()
    {
        Console.WriteLine("Esto simula una tarea cualquiera");
    }
}
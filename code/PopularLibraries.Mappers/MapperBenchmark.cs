using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Configs;
using BenchmarkDotNet.Order;

namespace PopularLibraries.Mappers;

[MemoryDiagnoser]
[KeepBenchmarkFiles(false)]
[GroupBenchmarksBy(BenchmarkLogicalGroupRule.ByMethod)]
[Orderer(SummaryOrderPolicy.FastestToSlowest)]
public class MapperBenchmark
{
    private readonly AccountInformationDto _accountDto = MapperHelper.BuildDefaultDto();
    private readonly AutomapperExample _automapperExample = new AutomapperExample();
    private readonly ManualMapperExample _manualMapperExample = new ManualMapperExample();
    private readonly MapperlyExample _mapperlyExample = new MapperlyExample();
    private readonly MapsterExample _mapsterExample = new MapsterExample();

    [Benchmark]
    public void Automapper()
    {
        _ = _automapperExample.Execute(_accountDto);
    }

    [Benchmark]
    public void Manual()
    {
        _ = _manualMapperExample.Execute(_accountDto);
    }

    [Benchmark]
    public void Mapperly()
    {
        _ = _mapperlyExample.Execute(_accountDto);
    }

    [Benchmark]
    public void Mapster()
    {
        _ = _mapsterExample.Execute(_accountDto);
    }
}
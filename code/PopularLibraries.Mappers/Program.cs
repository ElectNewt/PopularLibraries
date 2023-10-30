// See https://aka.ms/new-console-template for more information

using BenchmarkDotNet.Configs;
using BenchmarkDotNet.Reports;
using BenchmarkDotNet.Running;
using PopularLibraries.Mappers;

BenchmarkRunner.Run<MapperBenchmark>();
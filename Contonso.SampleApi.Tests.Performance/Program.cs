namespace Contonso.SampleApi.Tests.Performance;

using BenchmarkDotNet.Running;
using Contonso.SampleApi.Tests.Performance.Authors;

internal static class Program
{
    public static void Main(string[] args)
    {
        BenchmarkRunner.Run<AuthorTests>();
    }
}

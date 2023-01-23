namespace Contonso.SampleApi.Tests.Performance.Authors;

using BenchmarkDotNet.Attributes;
using Bogus.DataSets;
using Contonso.SampleApi.Application.Authors.Commands.CreateAuthor;
using Contonso.SampleApi.Application.Authors.Queries.GetAuthors;
using Contonso.SampleApi.Tests.Application.Common;

[MemoryDiagnoser]
public class AuthorTests : BaseTest
{
    [GlobalSetup]
    public void Setup()
    {
        for (var i = 0; i < 1000; i++)
        {
            var nameDataSet = new Name();

            var command = new CreateAuthorCommand
            {
                FirstName = null,
                LastName = nameDataSet.LastName(),
            };
            
            this.Mediator.Send(command);
        }
    }
    
    [Benchmark]
    public void GetAuthors1()
    {
        _ = this.Mediator.Send(new GetAuthorsQuery()).Result;
    }
}

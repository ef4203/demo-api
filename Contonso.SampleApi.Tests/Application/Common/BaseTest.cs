namespace Contonso.SampleApi.Tests.Application.Common;

using AutoMapper;
using Contonso.SampleApi.Application.Common.Abstraction;
using Contonso.SampleApi.Application.Common.Mapping;
using Contonso.SampleApi.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

public class BaseTest
{
    protected readonly IMapper Mapper;
    protected readonly IApplicationDbContext DbContext;

    public BaseTest()
    {
        var mapperConfiguration = new MapperConfiguration(config 
            => config.AddProfile<MappingProfile>());

        var dbContextOptions = new DbContextOptionsBuilder<ApplicationDbContext>()
            .UseInMemoryDatabase("ExampleApi")
            .Options;

        this.Mapper = mapperConfiguration.CreateMapper();
        this.DbContext = new ApplicationDbContext(dbContextOptions);
    }
}

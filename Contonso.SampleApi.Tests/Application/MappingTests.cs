namespace Contonso.SampleApi.Tests.Application;

using System.Runtime.Serialization;
using AutoMapper;
using Contonso.SampleApi.Application.Books.Queries.GetBooks;
using Contonso.SampleApi.Application.Common.Mapping;
using Contonso.SampleApi.Domain.Entities;

public class MappingTests
{
    private readonly IMapper mapper;

    public MappingTests()
    {
        var mapperConfiguration =
            new MapperConfiguration(config => config.AddProfile<MappingProfile>());

        this.mapper = mapperConfiguration.CreateMapper();
    }

    [Test]
    [TestCase(typeof(Book), typeof(BookDto))]
    public void ShouldSupportMappingFromSourceToDestination(Type source, Type destination)
    {
        _ = source ?? throw new ArgumentNullException(nameof(source));
        _ = destination ?? throw new ArgumentNullException(nameof(destination));

        var instance = GetInstanceOf(source);

        this.mapper.Map(instance, source, destination);
    }

    private static object GetInstanceOf(Type type)
    {
        return type.GetConstructor(Type.EmptyTypes) != null
            ? Activator.CreateInstance(type)!
            : FormatterServices.GetUninitializedObject(type);
    }
}
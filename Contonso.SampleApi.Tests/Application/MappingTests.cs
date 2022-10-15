namespace Contonso.SampleApi.Tests.Application;

using System.Runtime.Serialization;
using AutoMapper;
using Contonso.SampleApi.Application.Books.Queries.GetBooks;
using Contonso.SampleApi.Application.Common.Mapping;
using Contonso.SampleApi.Domain.Entities;
using Contonso.SampleApi.Tests.Application.Common;

public class MappingTests : BaseTest
{
    [Test]
    [TestCase(typeof(Book), typeof(BookDto))]
    public void ShouldSupportMappingFromSourceToDestination(Type source, Type destination)
    {
        _ = source ?? throw new ArgumentNullException(nameof(source));
        _ = destination ?? throw new ArgumentNullException(nameof(destination));

        var instance = GetInstanceOf(source);

        this.Mapper.Map(instance, source, destination);
    }

    private static object GetInstanceOf(Type type)
    {
        return type.GetConstructor(Type.EmptyTypes) != null
            ? Activator.CreateInstance(type)!
            : FormatterServices.GetUninitializedObject(type);
    }
}

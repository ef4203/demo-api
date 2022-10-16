namespace Contonso.SampleApi.Tests.Application;

using System.Runtime.Serialization;
using Contonso.SampleApi.Application.Authors.Queries.GetAuthors;
using Contonso.SampleApi.Application.Books.Queries.GetBooks;
using Contonso.SampleApi.Domain.Entities;
using Contonso.SampleApi.Tests.Application.Common;

public class MappingTests : BaseTest
{
    [Test]
    [TestCase(typeof(Book), typeof(BookDto))]
    [TestCase(typeof(Author), typeof(AuthorDto))]
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

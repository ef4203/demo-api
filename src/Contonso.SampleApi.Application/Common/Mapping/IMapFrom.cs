namespace Contonso.SampleApi.Application.Common.Mapping;

using AutoMapper;

public interface IMapFrom<T>
{
    void Mapping(Profile profile)
    {
        ArgumentNullException.ThrowIfNull(profile);

        profile.CreateMap(typeof(T), this.GetType());
    }
}

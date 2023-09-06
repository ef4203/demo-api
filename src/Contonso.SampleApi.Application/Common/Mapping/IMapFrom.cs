namespace Contonso.SampleApi.Application.Common.Mapping;

using AutoMapper;

public interface IMapFrom<T>
{
    void Mapping(Profile profile)
    {
        _ = profile ?? throw new ArgumentNullException(nameof(profile));

        profile.CreateMap(typeof(T), this.GetType());
    }
}

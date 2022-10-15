namespace Contonso.SampleApi.Domain.Common;

public interface IArchivable
{
    bool IsDeleted { get; set; }
}
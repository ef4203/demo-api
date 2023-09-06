namespace Contonso.SampleApi.Domain.Common;

public interface IModificationTracker
{
    DateTime ModifiedOn { get; set; }
}

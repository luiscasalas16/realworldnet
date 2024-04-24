namespace RealWorldBackendNet.Api.Features.Tags;

public interface ITagsHandler
{
    public Task<string[]> GetTagsAsync(CancellationToken cancellationToken);
}

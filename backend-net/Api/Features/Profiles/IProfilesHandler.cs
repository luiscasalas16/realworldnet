using RealWorldBackendNet.Core.Dto;

namespace RealWorldBackendNet.Api.Features.Profiles;

public interface IProfilesHandler
{
    public Task<ProfileDto> GetAsync(string profileUsername, string? username, CancellationToken cancellationToken);

    public Task<ProfileDto> FollowProfileAsync(string profileUsername, string username,
        CancellationToken cancellationToken);

    public Task<ProfileDto> UnFollowProfileAsync(string profileUsername, string username,
        CancellationToken cancellationToken);
}

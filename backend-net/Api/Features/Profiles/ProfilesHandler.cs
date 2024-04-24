﻿using RealWorldBackendNet.Core.Dto;
using RealWorldBackendNet.Core.Repositories;

namespace RealWorldBackendNet.Api.Features.Profiles;

public class ProfilesHandler : IProfilesHandler
{
    private readonly IConduitRepository _repository;

    public ProfilesHandler(IConduitRepository repository)
    {
        _repository = repository;
    }

    public async Task<ProfileDto> GetAsync(string profileUsername, string? username,
        CancellationToken cancellationToken)
    {
        var profileUser = await _repository.GetUserByUsernameAsync(profileUsername, cancellationToken);

        if (profileUser is null)
        {
            throw new ProblemDetailsException(422, "Profile not found");
        }

        var isFollowing = false;

        if (username is not null)
        {
            isFollowing = await _repository.IsFollowingAsync(profileUsername, username, cancellationToken);
        }

        return new ProfileDto(profileUser.Username, profileUser.Bio, profileUser.Image, isFollowing);
    }

    public async Task<ProfileDto> FollowProfileAsync(string profileUsername, string username,
        CancellationToken cancellationToken)
    {
        var profileUser = await _repository.GetUserByUsernameAsync(profileUsername, cancellationToken);

        if (profileUser is null)
            throw new ProblemDetailsException(422, "Profile not found");
        

        _repository.Follow(profileUsername, username);
        await _repository.SaveChangesAsync(cancellationToken);

        return new ProfileDto(profileUser.Username, profileUser.Bio, profileUser.Email, true);
    }

    public async Task<ProfileDto> UnFollowProfileAsync(string profileUsername, string username,
        CancellationToken cancellationToken)
    {
        var profileUser = await _repository.GetUserByUsernameAsync(profileUsername, cancellationToken);

        if (profileUser is null)
        {
            throw new ProblemDetailsException(422, "Profile not found");
        }

        _repository.UnFollow(profileUsername, username);
        await _repository.SaveChangesAsync(cancellationToken);

        return new ProfileDto(profileUser.Username, profileUser.Bio, profileUser.Email, false);
    }
}

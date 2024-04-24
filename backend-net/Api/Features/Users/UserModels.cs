namespace RealWorldBackendNet.Api.Features.Users;

public record UserEnvelope<T>([Required] T User);

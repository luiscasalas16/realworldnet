namespace RealWorldBackendNet.Infrastructure.Utils.Interfaces;

public interface ITokenGenerator
{
    public string CreateToken(string username);
}

using System.Security.Cryptography.X509Certificates;

namespace RealWorldBackendNet.Infrastructure.Utils.Interfaces;

public interface ICertificateProvider
{
    X509Certificate2 LoadFromUserStore(string thumbprint);
}

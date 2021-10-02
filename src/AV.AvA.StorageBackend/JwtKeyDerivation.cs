using Microsoft.AspNetCore.Cryptography.KeyDerivation;

namespace AV.AvA.StorageBackend;

public static class JwtKeyDerivation
{
    public static byte[] DeriveKey(string password, int bitLength = 256)
    {
        return KeyDerivation.Pbkdf2(
            password: password,
            salt: new byte[] { 0 },
            prf: KeyDerivationPrf.HMACSHA256,
            iterationCount: 10000,
            numBytesRequested: bitLength / 8);
    }
}

using Chaos.NaCl;

public static class SolanaKeyUtils
{
    public static byte[] GetPublicKeyFromPrivateKey(byte[] privateKey)
    {
        byte[] publicKey = new byte[32];
        Ed25519.KeyPairFromSeed(out publicKey, out _, privateKey[..32]); // first 32 bytes is seed
        return publicKey;
    }
}

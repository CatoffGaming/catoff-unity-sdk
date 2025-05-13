using UnityEngine;
using Solana.Unity.Rpc;
using Solana.Unity.Wallet.Utilities;

public class SolanaDemo : MonoBehaviour
{
    public SolanaWebBridge solanaWebBridge;
    public string toPublicKey = "YOUR_DESTINATION_ADDRESS";

    private void Start()
    {
        solanaWebBridge.SendTransaction(toPublicKey, 10000, "Hello Backpack!");
    }
}
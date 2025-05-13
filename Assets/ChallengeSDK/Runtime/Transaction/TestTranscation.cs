using System.Threading.Tasks;
using UnityEngine;
using Solana.Unity.Rpc;
using Solana.Unity.Rpc.Types;

public class TestTransaction : MonoBehaviour
{
    public string playerMnemonic = "your mnemonic phrase here";
    public string escrowAddress = "cat9J53v7uQm72681mHGmPDyx2hrEnGZcBeTA6WptYp";
    public ulong lamportsToSend = 10000; // 0.00001 SOL

    private async void Start()
    {
        await SendSolToEscrow(lamportsToSend);
    }

    private async Task SendSolToEscrow(ulong lamports)
    {
        // var rpcClient = ClientFactory.GetClient(Cluster.DevNet);
        // var transactionService = new Catoff.SDK.SolanaTransactionService(rpcClient, playerMnemonic, escrowAddress, Commitment.Confirmed);
        
        // bool success = await transactionService.TransferToEscrow(0, lamports, "Payment to escrow");
        // Debug.Log($"Transaction success: {success}");
    }
}

using System;
using System.Threading.Tasks;
using Solana.Unity.Rpc;
using Solana.Unity.Rpc.Builders;
using Solana.Unity.Rpc.Core.Http;
using Solana.Unity.Rpc.Models;
using Solana.Unity.Rpc.Types;
using Solana.Unity.Wallet;
using Solana.Unity.Programs;
using UnityEngine;

namespace Catoff.SDK
{
    public class SolanaTransactionService
    {
        private readonly IRpcClient rpcClient;
        private readonly Account senderAccount;
        private readonly PublicKey escrowPublicKey;
        private readonly Commitment commitment;

        public SolanaTransactionService(IRpcClient rpcClient, byte[] privateKey, byte[] publicKey, string escrowAddress, Commitment commitment = Commitment.Confirmed)
        {
            this.rpcClient = rpcClient;
            this.senderAccount = new Account(privateKey, publicKey);
            this.escrowPublicKey = new PublicKey(escrowAddress);
            this.commitment = commitment;
        }

        public async Task<bool> TransferToEscrow(ulong lamports, string memo = "")
        {
            try
            {
                Debug.Log($"Loaded wallet with public key: {senderAccount.PublicKey}");
                return await Transfer(senderAccount, escrowPublicKey, lamports, memo);
            }
            catch (Exception e)
            {
                Debug.LogError($"Error preparing transfer: {e.Message}");
                return false;
            }
        }

        private async Task<bool> Transfer(Account sender, PublicKey recipient, ulong lamports, string memo)
        {
            try
            {
                var blockHashResp = await rpcClient.GetLatestBlockHashAsync(commitment);
                if (!blockHashResp.WasSuccessful)
                {
                    Debug.LogError("Failed to get recent blockhash.");
                    return false;
                }

                string blockhash = blockHashResp.Result.Value.Blockhash;

                var txBuilder = new TransactionBuilder()
                    .SetRecentBlockHash(blockhash)
                    .SetFeePayer(sender.PublicKey)
                    .AddInstruction(SystemProgram.Transfer(sender.PublicKey, recipient, lamports));

                if (!string.IsNullOrEmpty(memo))
                    txBuilder.AddInstruction(MemoProgram.NewMemo(sender.PublicKey, memo));

                var transaction = txBuilder.Build(sender);

                var simResult = await rpcClient.SimulateTransactionAsync(transaction, commitment: Commitment.Confirmed);
                var sim = simResult.Result?.Value;

                if (!simResult.WasSuccessful || sim == null || sim.Error != null)
                {
                    Debug.LogError("Simulation failed:");
                    Debug.LogError(simResult.RawRpcResponse);
                    return false;
                }

                var sendTxResp = await rpcClient.SendTransactionAsync(transaction);
                if (!sendTxResp.WasSuccessful)
                {
                    Debug.LogError($"Failed to send transaction: {sendTxResp.Reason}");
                    return false;
                }

                Debug.Log($"Transaction sent successfully! Signature: {sendTxResp.Result}");
                return true;
            }
            catch (Exception ex)
            {
                Debug.LogError($"Transfer failed: {ex.Message}");
                return false;
            }
        }
    }
}

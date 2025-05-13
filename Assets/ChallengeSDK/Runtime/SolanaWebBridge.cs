using System.Runtime.InteropServices;
using UnityEngine;

public class SolanaWebBridge : MonoBehaviour
{
#if UNITY_WEBGL && !UNITY_EDITOR
    [DllImport("__Internal")]
    private static extern void RequestSolanaTransaction(string toPubkey, ulong lamports, string memo);
#endif

    public void SendTransaction(string toPubkey, ulong lamports, string memo)
    {
#if UNITY_WEBGL && !UNITY_EDITOR
        RequestSolanaTransaction(toPubkey, lamports, memo);
#else
        Debug.Log("WebGL only: Call skipped in Editor.");
#endif
    }
}
mergeInto(LibraryManager.library, {
    RequestSolanaTransaction: function (toPubkeyPtr, lamports, memoPtr) {
        var toPubkey = UTF8ToString(toPubkeyPtr);
        var memo = UTF8ToString(memoPtr);

        if (!window.backpack || !window.backpack.solana || !window.backpack.solana.isConnected) {
            if (window.backpack && window.backpack.solana) {
                window.backpack.solana.connect();
            } else {
                console.error("Backpack Wallet not found.");
                return;
            }
        }

        var connection = new solanaWeb3.Connection("https://api.devnet.solana.com");

        var fromPubkey = window.backpack.solana.publicKey;
        var toPublicKey = new solanaWeb3.PublicKey(toPubkey);

        var transaction = new solanaWeb3.Transaction().add(
            solanaWeb3.SystemProgram.transfer({
                fromPubkey: fromPubkey,
                toPubkey: toPublicKey,
                lamports: lamports
            })
        );

        transaction.feePayer = fromPubkey;

        connection.getLatestBlockhash().then(function(result) {
            transaction.recentBlockhash = result.blockhash;

            window.backpack.solana.signTransaction(transaction).then(function(signedTx) {
                connection.sendRawTransaction(signedTx.serialize()).then(function(sig) {
                    console.log("Transaction sent:", sig);
                }).catch(function(err) {
                    console.error("Sending failed:", err);
                });
            }).catch(function(err) {
                console.error("Signing failed:", err);
            });
        });
    }
});

using System;
using UnityEngine;
using UnityEngine.UI;
using System.Threading.Tasks;
using Catoff;

public class ChallengeUI : MonoBehaviour
{
    public InputField challengeNameField;
    public InputField descriptionField;
    public Button submitButton;
    public Text responseText;

    private ChallengeSDK challengeSDK;
    public string apiKey = "bc3b2e14b19749950803bdc53774e1db5b10565902f50652939ad3a683d9a13d";
    private string url = "https://sonicmainnet-apiv2.catoff.xyz/";

    void Start()
    {
        challengeSDK = new ChallengeSDK(url, apiKey);
        submitButton.onClick.AddListener(async () => await SubmitChallenge());
    }

    private async Task SubmitChallenge()
    {
        CreateChallengeDto challenge = new CreateChallengeDto
        {
            ChallengeName = challengeNameField.text,
            ChallengeDescription = descriptionField.text,
            StartDate = DateTimeOffset.UtcNow.ToUnixTimeMilliseconds(),
            EndDate = DateTimeOffset.UtcNow.AddDays(7).ToUnixTimeMilliseconds(),
            GameID = 1,
            MaxParticipants = 10,
            Wager = 50,
            Target = 100,
            ChallengeCreator = 123,
            AllowSideBets = true,
            SideBetsWager = 10,
            Unit = "points",
            IsPrivate = false,
            Currency = VERIFIED_CURRENCY.USDC,
            ChallengeCategory = "Sports",
            NFTMedia = "https://example.com/nft.png",
            Media = "https://example.com/media.mp4",
            ActualStartDate = DateTimeOffset.UtcNow.ToUnixTimeMilliseconds(),
            UserAddress = "0x1234567890abcdef"
        };

        string response = await challengeSDK.CreateChallenge(challenge);
        responseText.text = response ?? "Failed to create challenge.";
    }
}

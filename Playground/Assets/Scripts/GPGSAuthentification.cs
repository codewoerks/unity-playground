using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GooglePlayGames;
using GooglePlayGames.BasicApi;
using UnityEngine.SocialPlatforms;

public class GPGSAuthentification : MonoBehaviour
{
    public static PlayGamesPlatform platform;

    void Start()
    {
        if (platform == null)
        {
            PlayGamesClientConfiguration config = new PlayGamesClientConfiguration.Builder()
                
                .Build();

            PlayGamesPlatform.InitializeInstance(config);
            PlayGamesPlatform.DebugLogEnabled = true;

            PlayGamesPlatform.Activate();
            Debug.Log("Play Services: Initializing");

            Authenticate();
        }
    }

    public void Authenticate()
    {
        Social.Active.localUser.Authenticate(success =>
        {
            Debug.Log("Play Services: " + success.ToString());
            if (success)
            {
                Debug.Log("Logged in successfully");
            }
            else
            {
                Debug.LogError("Failed to log in :(");
            }
        });
    }
}

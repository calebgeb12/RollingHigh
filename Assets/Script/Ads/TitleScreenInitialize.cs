using UnityEngine;
using System.Collections;
using UnityEngine.Advertisements;

public class TitleScreenInitialize : MonoBehaviour, IUnityAdsInitializationListener
{
    [SerializeField] string _androidGameId;
    [SerializeField] string _iOSGameId;
    [SerializeField] bool _testMode = true;
    private string _gameId;
 
    void Awake()
    {
        InitializeAds();
    }
 
    public void InitializeAds()
    {

    #if UNITY_IOS
            _gameId = _iOSGameId;
    #elif UNITY_ANDROID
            _gameId = _androidGameId;
    #elif UNITY_EDITOR
            _gameId = _androidGameId; //Only for testing the functionality in the Editor
    #endif


        //ads should only be initialized once after opening game, not after every play restart
        if (!Advertisement.isInitialized && Advertisement.isSupported)
        {
            Advertisement.Initialize(_gameId, _testMode, this);
        }
    }

    public void OnInitializationComplete()
    {
        Debug.Log("1");
        //how often does this get called?

        // Debug.Log("Unity Ads initialization complete.");
    }
 
    public void OnInitializationFailed(UnityAdsInitializationError error, string message)
    {
        // Debug.Log($"Unity Ads Initialization Failed: {error.ToString()} - {message}");
    }
}

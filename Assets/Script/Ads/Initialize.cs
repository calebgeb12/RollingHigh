using UnityEngine;
using System.Collections;
using UnityEngine.Advertisements;
 

//for game over screen
public class Initialize : MonoBehaviour, IUnityAdsInitializationListener
{
    [SerializeField] string _androidGameId;
    [SerializeField] string _iOSGameId;
    [SerializeField] bool _testMode = true;
    private string _gameId;

    public RewardAds rewardAds;
    public RewardAds2 rewardAds2;
    public InterstitalAds interstitalAds;

    //for testing
    public GameObject greenFlash;
 
    void Awake()
    {
        // Debug.Log("activated");
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
        // Debug.Log("1");
        // Debug.Log("Unity Ads initialization complete.");
        // rewardAds.LoadAd();
        // rewardAds2.LoadAd();
        rewardAds.Awake();
        rewardAds2.Awake();
        interstitalAds.LoadAd();
    }
 
    public void OnInitializationFailed(UnityAdsInitializationError error, string message)
    {
        // Debug.Log($"Unity Ads Initialization Failed: {error.ToString()} - {message}");
    }

    IEnumerator flashGreen(){
        yield return new WaitForSeconds(1f);
        greenFlash.SetActive(false);
    }
}
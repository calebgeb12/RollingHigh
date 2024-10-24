using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.Advertisements;
 
public class RewardAds2 : MonoBehaviour, IUnityAdsLoadListener, IUnityAdsShowListener
{
    [SerializeField] Button adButton;
    [SerializeField] string _androidAdUnitId = "Rewarded_Android";
    [SerializeField] string _iOSAdUnitId = "Rewarded_iOS";
    string _adUnitId = null; // This will remain null for unsupported platforms

    private ballScript player;
    public RewardAds extraCoinsAd;

    //for testing
    public GameObject redFlash;
    public GameObject greenFlash;
    public GameObject blueFlash;
    private bool loaded = false;
    private bool playAd = false;
 
    public void Awake()
    {
        // Get the Ad Unit ID for the current platform:
#if UNITY_IOS
        _adUnitId = _iOSAdUnitId;
#elif UNITY_ANDROID
        _adUnitId = _androidAdUnitId;
#endif

        // Disable the button until the ad is ready to show
        if (Advertisement.isInitialized) {
            adButton.interactable = true;
        }

        else {
            adButton.interactable = false;
        }
    }

    void Start () {
        player = FindObjectOfType<ballScript>();
    }

    void Update() {

        //if you want to play reward ad when it hasn't been loaded
        if (playAd && loaded) {
            ShowAd();
            loaded = false;
            playAd = false;
        }
    }

    IEnumerator flashRed(){
        yield return new WaitForSeconds(1f);
    }
 
    // Call this public method when you want to get an ad ready to show.
    public void LoadAd()
    {
        // IMPORTANT! Only load content AFTER initialization (in this example, initialization is handled in a different script).
        // Debug.Log("Loading Ad: " + _adUnitId);
        Advertisement.Load(_adUnitId, this);
        adButton.interactable = true;
    }
 
    // If the ad successfully loads, add a listener to the buttons and enable them
    public void OnUnityAdsAdLoaded(string adUnitId)
    {
        // Debug.Log("2");
        loaded = true;
    }

    public void LoadThenShow() {
        playAd = true;
        LoadAd();
    }

    // Implement a method to execute when the user clicks the button:
    public void ShowAd()
    {
        // Debug.Log("3");
        Advertisement.Show(_adUnitId, this);

        // extraCoinsAd.LoadAd();

        // Debug.Log("3 " + playExtraLifeAd + " " + loaded);
        // Disable the button:

        // Then show the ad:
        loaded = false;
        adButton.gameObject.SetActive(false);  

        //have to set both these to false so that ad is loaded when reward ad is chosen again
        playAd = false;
    }
 
    // Implement the Show Listener's OnUnityAdsShowComplete callback method to determine if the user gets a reward:
    public void OnUnityAdsShowComplete(string adUnitId, UnityAdsShowCompletionState showCompletionState)
    {   
        //grant a reward (double coins)
        if (adUnitId.Equals(_adUnitId) && showCompletionState.Equals(UnityAdsShowCompletionState.COMPLETED))
        {
            player.doubleCoins();    
        }
    }

    public void changePosition() {
        adButton.gameObject.transform.localPosition = new Vector3(0f, -338f, 0f); 
    }
 
    // Implement Load and Show Listener error callbacks:
    public void OnUnityAdsFailedToLoad(string adUnitId, UnityAdsLoadError error, string message)
    {
        // redFlash.SetActive(true);
        Debug.Log($"Error loading Ad Unit {adUnitId}: {error.ToString()} - {message}");
        // Use the error details to determine whether to try to load another ad.
    }
 
    public void OnUnityAdsShowFailure(string adUnitId, UnityAdsShowError error, string message)
    {
        // Debug.Log($"Error showing Ad Unit {adUnitId}: {error.ToString()} - {message}");
        // Use the error details to determine whether to try to load another ad.
    }
 
    public void OnUnityAdsShowStart(string adUnitId) { }
    public void OnUnityAdsShowClick(string adUnitId) { }
 
    void OnDestroy()
    {
        // Clean up the button listeners:
        adButton.onClick.RemoveAllListeners();
    }
}
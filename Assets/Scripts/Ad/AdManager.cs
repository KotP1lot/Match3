using UnityEngine;
using UnityEngine.Advertisements;

public class AdManager : MonoBehaviour, IUnityAdsInitializationListener
{
    [SerializeField] private string androindGameID;
    [SerializeField] private string rewardedID;
    [SerializeField] private string interstitalID;
    [SerializeField] private bool isTesting;

    public AdvertisementBase rewarded;
    public AdvertisementBase interstitial;

    public static AdManager Instance { get; private set; }

    private void Awake()
    {
        if(Instance != null && Instance != this) 
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
        DontDestroyOnLoad(gameObject);

        if (!Advertisement.isInitialized && Advertisement.isSupported) 
        {
            Advertisement.Initialize(androindGameID, isTesting, this);
        }
        rewarded = new AdvertisementBase();
        rewarded.Setup(rewardedID);
        rewarded.LoadAd();
        
        interstitial = new AdvertisementBase();
        interstitial.Setup(interstitalID);
        interstitial.LoadAd();
    }

    public void OnInitializationComplete()
    { }

    public void OnInitializationFailed(UnityAdsInitializationError error, string message)
    { }
}

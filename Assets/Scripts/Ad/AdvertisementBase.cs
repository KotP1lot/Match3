using System;
using UnityEngine.Advertisements;

public class AdvertisementBase : IUnityAdsLoadListener, IUnityAdsShowListener
{
    private string adUnitId;
    private Action action;

    public void Setup(string adUnitId)
    {
        this.adUnitId = adUnitId;
    }

    public void LoadAd()
    {
        Advertisement.Load(adUnitId, this);
    }
    public void ShowAd(Action action)
    {
        this.action = action;
        Advertisement.Show(adUnitId, this);
        LoadAd();
    }

    public void OnUnityAdsAdLoaded(string placementId)
    { }

    public void OnUnityAdsFailedToLoad(string placementId, UnityAdsLoadError error, string message)
    { }

    public void OnUnityAdsShowFailure(string placementId, UnityAdsShowError error, string message)
    { }

    public void OnUnityAdsShowStart(string placementId)
    { }

    public void OnUnityAdsShowClick(string placementId)
    { }
    public void OnUnityAdsShowComplete(string placementId, UnityAdsShowCompletionState showCompletionState)
    {
        if (placementId == adUnitId && showCompletionState.Equals(UnityAdsCompletionState.COMPLETED)) 
        {
            action?.Invoke();
        }
    }
}

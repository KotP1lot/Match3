using System;
using UnityEngine;
using UnityEngine.Advertisements;

public class AdvertisementBase : IUnityAdsLoadListener, IUnityAdsShowListener
{
    public Action<bool> OnAdLoaded;
    private string adUnitId;
    private Action successAction;
    private Action failureAction;

    public void Setup(string adUnitId)
    {
        this.adUnitId = adUnitId;
    }

    public void LoadAd()
    {
        Advertisement.Load(adUnitId, this);
    }
    public void ShowAd(Action success, Action failure)
    {
        this.successAction = success;
        this.failureAction = failure;
        Advertisement.Show(adUnitId, this);
        LoadAd();
    }

    public void OnUnityAdsAdLoaded(string placementId)
    {
    }

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
        if (placementId == adUnitId && showCompletionState == UnityAdsShowCompletionState.COMPLETED) 
        {
            successAction?.Invoke();
        }
        if (placementId == adUnitId 
            && (showCompletionState == UnityAdsShowCompletionState.SKIPPED 
            || showCompletionState == UnityAdsShowCompletionState.UNKNOWN))
        {
            failureAction?.Invoke();
        }
    }
}

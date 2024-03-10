using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IronSourceManager : SceneSingleton<IronSourceManager>
{
    [SerializeField] private string m_appKey;
    [SerializeField] private bool m_isTest;
    private const string testAppKey = "85460dcd";

    //Delegates
    public delegate void OnRewardedAdWatched();
    public OnRewardedAdWatched Entity_OnRewardedAdWatched;

    protected override void OnStart()
    {
        base.OnStart();
        var appKey = m_isTest ? testAppKey : m_appKey;

        IronSource.Agent.validateIntegration();

        IronSource.Agent.init(appKey, IronSourceAdUnits.REWARDED_VIDEO, IronSourceAdUnits.INTERSTITIAL, IronSourceAdUnits.BANNER);
        //For Rewarded Video
        IronSource.Agent.init(appKey, IronSourceAdUnits.REWARDED_VIDEO);
        //For Interstitial
        IronSource.Agent.init(appKey, IronSourceAdUnits.INTERSTITIAL);
        //For Banners
        IronSource.Agent.init(appKey, IronSourceAdUnits.BANNER);
        IronSourceEvents.onSdkInitializationCompletedEvent += SdkInitializationCompletedEvent;
        IronSource.Agent.loadInterstitial();
        IronSource.Agent.loadRewardedVideo();

    }

    private void SdkInitializationCompletedEvent()
    {

    }

    private void OnEnable()
    {
        IronSourceRewardedVideoEvents.onAdOpenedEvent += RewardedVideoOnAdOpenedEvent;
        IronSourceRewardedVideoEvents.onAdClosedEvent += RewardedVideoOnAdClosedEvent;
        IronSourceRewardedVideoEvents.onAdAvailableEvent += RewardedVideoOnAdAvailable;
        IronSourceRewardedVideoEvents.onAdUnavailableEvent += RewardedVideoOnAdUnavailable;
        IronSourceRewardedVideoEvents.onAdShowFailedEvent += RewardedVideoOnAdShowFailedEvent;
        IronSourceRewardedVideoEvents.onAdRewardedEvent += RewardedVideoOnAdRewardedEvent;
        IronSourceRewardedVideoEvents.onAdClickedEvent += RewardedVideoOnAdClickedEvent;
    }

    public void ShowInterstitialAd()
    {
        if (IronSource.Agent.isInterstitialReady())
        {
            IronSource.Agent.showInterstitial();
            IronSource.Agent.loadInterstitial();
        }
        else
        {
            IronSource.Agent.loadInterstitial();
        }
    }

    public void ShowRewardedVideoAd()
    {
#if UNITY_EDITOR
        Entity_OnRewardedAdWatched?.Invoke();
#endif
        //Time.timeScale = 0.01f;
        if (IronSource.Agent.isRewardedVideoAvailable())
        {
            IronSource.Agent.showRewardedVideo();
        }
    }

    void OnApplicationPause(bool isPaused)
    {
        IronSource.Agent.onApplicationPause(isPaused);
    }

    #region AdInfo Rewarded Video
    void RewardedVideoOnAdOpenedEvent(IronSourceAdInfo adInfo)
    {
        Debug.Log("unity-script: I got RewardedVideoOnAdOpenedEvent With AdInfo " + adInfo);
    }

    void RewardedVideoOnAdClosedEvent(IronSourceAdInfo adInfo)
    {
        Debug.Log("unity-script: I got RewardedVideoOnAdClosedEvent With AdInfo " + adInfo);
    }

    void RewardedVideoOnAdAvailable(IronSourceAdInfo adInfo)
    {
        Debug.Log("unity-script: I got RewardedVideoOnAdAvailable With AdInfo " + adInfo);
    }

    void RewardedVideoOnAdUnavailable()
    {
        Debug.Log("unity-script: I got RewardedVideoOnAdUnavailable");
    }

    void RewardedVideoOnAdShowFailedEvent(IronSourceError ironSourceError, IronSourceAdInfo adInfo)
    {
        Debug.Log("unity-script: I got RewardedVideoAdOpenedEvent With Error" + ironSourceError + "And AdInfo " + adInfo);
    }

    void RewardedVideoOnAdRewardedEvent(IronSourcePlacement ironSourcePlacement, IronSourceAdInfo adInfo)
    {
        Debug.Log("unity-script: I got RewardedVideoOnAdRewardedEvent With Placement" + ironSourcePlacement + "And AdInfo " + adInfo);
        Entity_OnRewardedAdWatched?.Invoke();
    }

    void RewardedVideoOnAdClickedEvent(IronSourcePlacement ironSourcePlacement, IronSourceAdInfo adInfo)
    {
        Debug.Log("unity-script: I got RewardedVideoOnAdClickedEvent With Placement" + ironSourcePlacement + "And AdInfo " + adInfo);
    }

    #endregion
}

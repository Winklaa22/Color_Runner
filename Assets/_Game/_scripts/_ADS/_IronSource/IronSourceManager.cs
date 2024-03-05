using System;
using UnityEngine;
using System.Runtime.InteropServices;


public class IronSourceManager : SceneSingleton<IronSourceManager>
{
    private static string appKey = "1d9b84785"; 

    private bool _isRewardedAvailable = false;
    private bool _isBannerAvailable = false;
    private bool _showBannerImmidiately = false;
    private bool _initialized = false;
    private IRewardCallBack _Delegate;

    // Use this for initialization
    protected override void OnAwake()
    {
        DontDestroyOnLoad(gameObject);

    }

    //Debug Method
    private void IronSourceEvents_onSdkInitializationCompletedEvent()
    {
        IronSourceEvents.onSdkInitializationCompletedEvent -= IronSourceEvents_onSdkInitializationCompletedEvent;
        Debug.LogFormat("IronsourceManager: SDK Initialization Complete");
        //InitializeEvents(); //oldy -- 
        _initialized = true;
    }

    //Debug Method
    private void InitializeEvents()
    {
        Debug.LogFormat("IronsourceManager: InitializeEvents");


        IronSourceEvents.onBannerAdLoadedEvent += BannerAdLoadedEvent;
        IronSourceEvents.onBannerAdLoadFailedEvent += BannerAdLoadFailedEvent;
        IronSourceEvents.onBannerAdClickedEvent += BannerAdClickedEvent;
        IronSourceEvents.onBannerAdScreenPresentedEvent += BannerAdScreenPresentedEvent;
        IronSourceEvents.onBannerAdScreenDismissedEvent += BannerAdScreenDismissedEvent;
        IronSourceEvents.onBannerAdLeftApplicationEvent += BannerAdLeftApplicationEvent;

        //Interstitials Events
        IronSourceEvents.onInterstitialAdReadyEvent += InterstitialAdReadyEvent;
        IronSourceEvents.onInterstitialAdLoadFailedEvent += InterstitialAdLoadFailedEvent;
        IronSourceEvents.onInterstitialAdShowSucceededEvent += InterstitialAdShowSucceededEvent;
        IronSourceEvents.onInterstitialAdShowFailedEvent += InterstitialAdShowFailedEvent;
        IronSourceEvents.onInterstitialAdClickedEvent += InterstitialAdClickedEvent;
        IronSourceEvents.onInterstitialAdOpenedEvent += InterstitialAdOpenedEvent;
        IronSourceEvents.onInterstitialAdClosedEvent += InterstitialAdClosedEvent;

        //Rewarded Events
        IronSourceEvents.onRewardedVideoAdOpenedEvent += RewardedVideoAdOpenedEvent;
        IronSourceEvents.onRewardedVideoAdClosedEvent += RewardedVideoAdClosedEvent;
        IronSourceEvents.onRewardedVideoAvailabilityChangedEvent += RewardedVideoAvailabilityChangedEvent;
        IronSourceEvents.onRewardedVideoAdStartedEvent += RewardedVideoAdStartedEvent;
        IronSourceEvents.onRewardedVideoAdEndedEvent += RewardedVideoAdEndedEvent;
        IronSourceEvents.onRewardedVideoAdRewardedEvent += RewardedVideoAdRewardedEvent;
        IronSourceEvents.onRewardedVideoAdShowFailedEvent += RewardedVideoAdShowFailedEvent;
        IronSource.Agent.loadBanner(IronSourceBannerSize.BANNER, IronSourceBannerPosition.BOTTOM);
        IronSource.Agent.loadInterstitial();
    }

    protected override void OnStart()
    {
        IronSource.Agent.init(appKey, IronSourceAdUnits.BANNER, IronSourceAdUnits.INTERSTITIAL, IronSourceAdUnits.REWARDED_VIDEO);
        IronSourceEvents.onSdkInitializationCompletedEvent += IronSourceEvents_onSdkInitializationCompletedEvent;

        //Interstitials Events
        IronSourceEvents.onInterstitialAdReadyEvent += InterstitialAdReadyEvent;
        IronSourceEvents.onInterstitialAdLoadFailedEvent += InterstitialAdLoadFailedEvent;
        IronSourceEvents.onInterstitialAdShowSucceededEvent += InterstitialAdShowSucceededEvent;
        IronSourceEvents.onInterstitialAdShowFailedEvent += InterstitialAdShowFailedEvent;
        IronSourceEvents.onInterstitialAdClickedEvent += InterstitialAdClickedEvent;
        IronSourceEvents.onInterstitialAdOpenedEvent += InterstitialAdOpenedEvent;
        IronSourceEvents.onInterstitialAdClosedEvent += InterstitialAdClosedEvent;

        //Rewarded Events
        IronSourceEvents.onRewardedVideoAdOpenedEvent += RewardedVideoAdOpenedEvent;
        IronSourceEvents.onRewardedVideoAdClosedEvent += RewardedVideoAdClosedEvent;
        IronSourceEvents.onRewardedVideoAvailabilityChangedEvent += RewardedVideoAvailabilityChangedEvent;
        IronSourceEvents.onRewardedVideoAdStartedEvent += RewardedVideoAdStartedEvent;
        IronSourceEvents.onRewardedVideoAdEndedEvent += RewardedVideoAdEndedEvent;
        IronSourceEvents.onRewardedVideoAdRewardedEvent += RewardedVideoAdRewardedEvent;
        IronSourceEvents.onRewardedVideoAdShowFailedEvent += RewardedVideoAdShowFailedEvent;

        IronSource.Agent.loadInterstitial();
        IronSource.Agent.loadRewardedVideo();

        Debug.LogFormat("IronSourceManager: Validate Integration");
        IronSource.Agent.validateIntegration();
    }

    //IronSource mandatory
    void OnApplicationPause(bool isPaused)
    {
        IronSource.Agent.onApplicationPause(isPaused);
    }

    public void showBannerAd()
    {
        Debug.Log("IronSourceManager: Requested Banner Ad");

        Debug.Log("Show banner ads");
        if (_isBannerAvailable)
        {
            Debug.Log("IronSourceManager: Banner Ad is available. Displaying.");
            IronSource.Agent.displayBanner();

            Debug.Log("IronSourceManager: Requesting next Ad");
            IronSource.Agent.loadBanner(IronSourceBannerSize.BANNER, IronSourceBannerPosition.BOTTOM);
            _showBannerImmidiately = false;
        }
        else
        {
            Debug.Log("IronSourceManager: Banner Ad is not available. Loading");
            IronSource.Agent.loadBanner(IronSourceBannerSize.BANNER, IronSourceBannerPosition.BOTTOM);
            //Debug.Log("IronSourceManager: Try to display banner.");
            //IronSource.Agent.displayBanner();

            //Debug.Log("IronSourceManager: Raise a flag to play banner immidiately.");
            //_showBannerImmidiately = true;

            IronSource.Agent.displayBanner();
        }
    }

    public void hideBannerAd()
    {
        Debug.Log("hideBanner");
        IronSource.Agent.hideBanner();
    }

    public void destroyBannerAd()
    {
        Debug.Log("destroyBanner");
        IronSource.Agent.destroyBanner();
        _isBannerAvailable = false;
    }

    public void showInterstitialAd()
    {
        Debug.Log("show insterstial");

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



    public void showRewardedVideoAd(IRewardCallBack _delegate)
    {
        if (Application.isEditor)
        {
            _delegate.GiveReward();
            return;
        }


#if UNITY_EDITOR
        _delegate.GiveReward();
#endif
        if (IronSource.Agent.isRewardedVideoAvailable())
        {
            _Delegate = _delegate;
            IronSource.Agent.showRewardedVideo();
        }
        else
        {
            Debug.Log("IronSourceManager --> showRewardedVideo");

        }
    }

    private void BannerAdLoadedEvent()
    {
        _isBannerAvailable = true;
        Debug.Log("IronSourceManager --> Callback: BannerAdLoadedEvent()");
        if (_showBannerImmidiately)
        {
            Debug.Log("IronSourceManager: Displaying Banner immidiately, as requested.");
            showBannerAd();
        }

    }

    private void BannerAdLoadFailedEvent(IronSourceError error)
    {
        Debug.Log("EpicWinAdsManager --> Callback: BannerAdLoadFailedEvent() error: " + error);

    }

    // Invoked when end user clicks on the banner ad
    private void BannerAdClickedEvent()
    {
        Debug.Log("EpicWinAdsManager --> Callback: BannerAdClickedEvent()");
        // TODO
    }

    //Notifies the presentation of a full screen content following user click
    private void BannerAdScreenPresentedEvent()
    {
        Debug.Log("IronSourceManager --> Callback: BannerAdScreenPresentedEvent()");
        // TODO
    }

    //Notifies the presented screen has been dismissed
    private void BannerAdScreenDismissedEvent()
    {
        Debug.Log("EpicWinAdsManager --> Callback: BannerAdScreenDismissedEvent()");
        // TODO
    }

    //Invoked when the user leaves the app
    private void BannerAdLeftApplicationEvent()
    {
        Debug.Log("EpicWinAdsManager --> Callback: BannerAdLeftApplicationEvent()");
        // TODO
    }




    private void InterstitialAdLoadFailedEvent(IronSourceError error)
    {
        Debug.Log("IronSourceManager --> Callback: InterstitialAdLoadFailedEvent()");

    }


    private void InterstitialAdShowSucceededEvent()
    {
        Debug.Log("IronSourceManager --> Callback: InterstitialAdShowSucceededEvent()");

    }


    private void InterstitialAdShowFailedEvent(IronSourceError error)
    {
        Debug.Log("IronSourceManager --> Callback: InterstitialAdShowFailedEvent() error: " + error);
        IronSource.Agent.loadInterstitial();
    }

    private void InterstitialAdClickedEvent()
    {
        Debug.Log("IronSourceManager --> Callback: InterstitialAdClickedEvent()");
    }

    private void InterstitialAdClosedEvent()
    {
        Debug.Log("IronSourceManager --> Callback: InterstitialAdClosedEvent()");
        IronSource.Agent.loadInterstitial();
    }

    private void InterstitialAdReadyEvent()
    {
        Debug.Log("IronSourceManager --> Callback: InterstitialAdReadyEvent()");

    }


    private void InterstitialAdOpenedEvent()
    {
        Debug.Log("IronSourceManager --> Callback: InterstitialAdOpenedEvent()");

    }



    private void RewardedVideoAdOpenedEvent()
    {
        Debug.Log("IronSourceManager --> Callback: RewardedVideoAdOpenedEvent()");
    }

    private void RewardedVideoAdClosedEvent()
    {
        Debug.Log("IronSourceManager --> Callback: RewardedVideoAdClosedEvent()");
	
    }

    private void RewardedVideoAvailabilityChangedEvent(bool available)
    {
        _isRewardedAvailable = available;
        Debug.Log("IronSourceManager --> Callback: RewardedVideoAvailabilityChangedEvent() available:" + available);
    }

    private void RewardedVideoAdStartedEvent()
    {
        Debug.Log("IronSourceManager --> Callback: RewardedVideoAdStartedEvent()");
    }

    private void RewardedVideoAdEndedEvent()
    {
        Debug.Log("IronSourceManager --> Callback: RewardedVideoAdEndedEvent()");
    }

    private void RewardedVideoAdRewardedEvent(IronSourcePlacement placement)
    {
        _Delegate?.GiveReward();

        _Delegate = null;
        Time.timeScale = 1;
    }

    private void RewardedVideoAdShowFailedEvent(IronSourceError error)
    {
        Debug.Log("IronSourceManager --> Callback: RewardedVideoAdShowFailedEvent() error: " + error);
    }


}


public interface IRewardCallBack
{
    void GiveReward();
}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class AdsManager : MonoBehaviour
{

    public string mySdkKey = "2iI3thL0CMAKQuqnkg8NpvGtmfann8znXJARMqMgcg2exZQtCrsbxALJLniVRw_fxTzE2OMCsPhJ-wr39df4Av";
    private string adInterstitialUnitId = "e6398e44c0b6b826";
    //private string RewardedadUnitId = "";
    private string BanneradUnitId = "fb49da2c56deb3ae";
    //int _rewardedretryAttempt;
    int _interstitialretryAttempt;

    public static AdsManager instance;



    // inter interval time
    private float timeBetweenShots = 15f;
    private float timeStamp = 0;
    private bool canShowInter = false;


    private void Awake()
    {
        MaxSdk.SetSdkKey(mySdkKey);
        // MaxSdk.SetHasUserConsent(true);
        MaxSdk.SetIsAgeRestrictedUser(false);
        MaxSdk.SetDoNotSell(false);

        MaxSdkCallbacks.OnSdkInitializedEvent += (MaxSdkBase.SdkConfiguration sdkConfiguration) =>
        {
            // AppLovin SDK is initialized, start loading ads






            InitializeInterstitialAds();
            //  InitializeRewardedAds();

            InitializeBannerAds();
        };


    void Start()
    {

            

       }



        MaxSdk.InitializeSdk();





        if (instance == null)
        {
            instance = this;
        }
        else if (instance != this)
        {
            Destroy(gameObject);
        }
        DontDestroyOnLoad(gameObject);

    }

    private void Update()
    {
       


    }

    #region Banner

    private void InitializeBannerAds()
    {
        MaxSdk.CreateBanner(BanneradUnitId,MaxSdkBase.BannerPosition.BottomCenter);
        MaxSdk.SetBannerBackgroundColor(BanneradUnitId,Color.gray) ;
    }

    public void ShowBanner()
    {
        MaxSdk.ShowBanner(BanneradUnitId);
        print("show banner");
    }

    public void HideBanner()
    {
        MaxSdk.HideBanner(BanneradUnitId);
        print("hide banner");

    }

    #endregion
    
    
    
    #region Interstitial 
    public void InitializeInterstitialAds()
    {
        // Attach callback
        MaxSdkCallbacks.OnInterstitialLoadedEvent += OnInterstitialLoadedEvent;
        MaxSdkCallbacks.OnInterstitialLoadFailedEvent += OnInterstitialFailedEvent;
        MaxSdkCallbacks.OnInterstitialAdFailedToDisplayEvent += InterstitialFailedToDisplayEvent;
        MaxSdkCallbacks.OnInterstitialHiddenEvent += OnInterstitialDismissedEvent;

        // Load the first interstitial
        LoadInterstitial();
    }

    private void LoadInterstitial()
    {
        MaxSdk.LoadInterstitial(adInterstitialUnitId);
    }

    private void OnInterstitialLoadedEvent(string adUnitId)
    {
        // Interstitial ad is ready to be shown. MaxSdk.IsInterstitialReady(adUnitId) will now return 'true'

        // Reset retry attempt
        _interstitialretryAttempt = 0;
    }

    private void OnInterstitialFailedEvent(string adUnitId, int errorCode)
    {
        // Interstitial ad failed to load 
        // We recommend retrying with exponentially higher delays up to a maximum delay (in this case 64 seconds)

        _interstitialretryAttempt++;
        double retryDelay = Math.Pow(2, Math.Min(6, _interstitialretryAttempt));

        Invoke("LoadInterstitial", (float)retryDelay);
    }

    private void InterstitialFailedToDisplayEvent(string adUnitId, int errorCode)
    {
        // Interstitial ad failed to display. We recommend loading the next ad
        LoadInterstitial();
    }

    private void OnInterstitialDismissedEvent(string adUnitId)
    {
        // Interstitial ad is hidden. Pre-load the next ad
        LoadInterstitial();
    }
    public void ShowInterstitial()
    {
        if (MaxSdk.IsInterstitialReady(adInterstitialUnitId) )
        {
            MaxSdk.ShowInterstitial(adInterstitialUnitId);
           
        }
        else
        {

            LoadInterstitial();
        }
    }

    #endregion



    //#region Rewarded video





    //public void InitializeRewardedAds()
    //{
    //    // Attach callback
    //    MaxSdkCallbacks.OnRewardedAdLoadedEvent += OnRewardedAdLoadedEvent;
    //    MaxSdkCallbacks.OnRewardedAdLoadFailedEvent += OnRewardedAdFailedEvent;
    //    MaxSdkCallbacks.OnRewardedAdFailedToDisplayEvent += OnRewardedAdFailedToDisplayEvent;
    //    MaxSdkCallbacks.OnRewardedAdDisplayedEvent += OnRewardedAdDisplayedEvent;
    //    MaxSdkCallbacks.OnRewardedAdClickedEvent += OnRewardedAdClickedEvent;
    //    MaxSdkCallbacks.OnRewardedAdHiddenEvent += OnRewardedAdDismissedEvent;
    //    MaxSdkCallbacks.OnRewardedAdReceivedRewardEvent += OnRewardedAdReceivedRewardEvent;

    //    // Load the first rewarded ad
    //    LoadRewardedAd();
    //}

    //private void LoadRewardedAd()
    //{
    //    MaxSdk.LoadRewardedAd(RewardedadUnitId);
    //}

    //private void OnRewardedAdLoadedEvent(string adUnitId)
    //{
    //    // Rewarded ad is ready to be shown. MaxSdk.IsRewardedAdReady(adUnitId) will now return 'true'

    //    // Reset retry attempt
    //    _rewardedretryAttempt = 0;
    //}

    //private void OnRewardedAdFailedEvent(string adUnitId, int errorCode)
    //{
    //    // Rewarded ad failed to load 
    //    // We recommend retrying with exponentially higher delays up to a maximum delay (in this case 64 seconds)

    //    _rewardedretryAttempt++;
    //    double retryDelay = Math.Pow(2, Math.Min(6, _rewardedretryAttempt));

    //    Invoke("LoadRewardedAd", (float)retryDelay);
    //}

    //private void OnRewardedAdFailedToDisplayEvent(string adUnitId, int errorCode)
    //{
    //    // Rewarded ad failed to display. We recommend loading the next ad
    //    LoadRewardedAd();
    //}

    //private void OnRewardedAdDisplayedEvent(string adUnitId)
    //{
    //    //  GameManager.Instance.RespawnPlayer();
    //}

    //private void OnRewardedAdClickedEvent(string adUnitId)
    //{

    //    //   GameManager.Instance.RespawnPlayer();

    //}

    //private void OnRewardedAdDismissedEvent(string adUnitId)
    //{
    //    // Rewarded ad is hidden. Pre-load the next ad

    //    // LoadRewardedAd();
    //}

    //private void OnRewardedAdReceivedRewardEvent(string adUnitId, MaxSdk.Reward reward)
    //{
    //   // GameManager.Instance.RespawnPlayer();
    //}


    //public void ShowRewardedVideo()
    //{
    //    if (MaxSdk.IsRewardedAdReady(RewardedadUnitId))
    //    {
    //        MaxSdk.ShowRewardedAd(RewardedadUnitId);
    //    }
    //    else
    //    {
    //        // Rewarded ad is hidden. Pre-load the next ad
    //        LoadRewardedAd();
    //    }
    //}

    //public void TestIntegration()
    //{
    //    // Show Mediation Debugger
    //    MaxSdk.ShowMediationDebugger();
    //}


    //#endregion
    void OnDestroy()
    {

    }



}

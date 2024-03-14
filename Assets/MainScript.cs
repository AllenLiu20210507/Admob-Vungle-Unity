using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GoogleMobileAds.Api;
using UnityEngine.UI;
using System;
using GoogleMobileAds.Api.Mediation.Vungle;

public class MainScript : MonoBehaviour
{

    private InterstitialAd interstitial;
    private RewardBasedVideoAd rewardBasedVideo;

    string appId = Constant.APP_ID;
    string adUnitId = Constant.AD_UNIT_ID;
    string interstitialAdUnitId = Constant.INTERSTITIAL_UNIT_ID;
    string rewardAdUnitId = Constant.REWARD_UNIT_ID;
    string bannerAdUnitId = Constant.BANNER_UNIT_ID;
    string mrecAdUnitId = Constant.MREC_UNIT_ID;
    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnGUI()
    {
        GUI.backgroundColor = Color.green;
        GUILayoutOption[] option = new GUILayoutOption[]
        {
            GUILayout.Height(80)
        };
        Texture2D texture = new Texture2D(128, 128);

        GUIStyle style = new GUIStyle(GUI.skin.button);
        style.normal.background = texture;
        style.normal.textColor = Color.yellow;
        style.fontSize = 48;

        style.active.textColor = Color.blue;

        GUILayout.BeginArea(new Rect(0, 0, Screen.width, Screen.height));
        GUILayout.BeginVertical();

        GUILayout.Space(20);
        GUIStyle lableStyle = new GUIStyle();
        lableStyle.fontSize = 56;
        lableStyle.alignment = TextAnchor.MiddleCenter;
        GUILayout.Label("Mopub + Vungle", lableStyle);

        if (GUILayout.Button("Init", style, option))
        {
            initSDK();
        }

        GUILayout.Space(20);
        GUILayout.BeginHorizontal();

        if (GUILayout.Button("Load Interstitial", style, option))
        {
            RequestInterstitial();
        }
        if (GUILayout.Button("Play Interstitial", style, option))
        {
            ShowInterstitial();
        }
        GUILayout.EndHorizontal();

        GUILayout.Space(20);
        GUILayout.BeginHorizontal();
        if (GUILayout.Button("Load Reward", style, option))
        {
            RequestRewardBasedVideo();
        }
        if (GUILayout.Button("Play Reward", style, option))
        {
            ShowRewardBasedVideo();
        }
        GUILayout.EndHorizontal();

        GUILayout.Space(20);
        GUILayout.BeginHorizontal();
        if (GUILayout.Button("Load Banner", style, option))
        {
            //loadBanner();
        }
        if (GUILayout.Button("Play Banner", style, option))
        {
            //playBanner();
        }
        if (GUILayout.Button("Destroy Banner", style, option))
        {
            //MoPub.DestroyBanner(bannerAdUnitId);
        }
        GUILayout.EndHorizontal();

        GUILayout.Space(20);

        GUILayout.BeginHorizontal();
        if (GUILayout.Button("Load MREC", style, option))
        {
            //loadMREC();
        }
        if (GUILayout.Button("Play MREC", style, option))
        {
            //playMREC();
        }
        if (GUILayout.Button("Destroy MREC", style, option))
        {
            //MoPub.DestroyBanner(mrecAdUnitId);
        }
        GUILayout.EndHorizontal();
        GUILayout.EndVertical();
        GUILayout.EndArea();
    }

    private void initSDK()
    {
        MobileAds.Initialize(appId);

        // Get singleton reward based video ad reference.
        this.rewardBasedVideo = RewardBasedVideoAd.Instance;


        // Called when an ad request has successfully loaded.
        rewardBasedVideo.OnAdLoaded += HandleRewardBasedVideoLoaded;
        // Called when an ad request failed to load.
        rewardBasedVideo.OnAdFailedToLoad += HandleRewardBasedVideoFailedToLoad;
        // Called when an ad is shown.
        rewardBasedVideo.OnAdOpening += HandleRewardBasedVideoOpened;
        // Called when the ad starts to play.
        rewardBasedVideo.OnAdStarted += HandleRewardBasedVideoStarted;
        // Called when the user should be rewarded for watching a video.
        rewardBasedVideo.OnAdRewarded += HandleRewardBasedVideoRewarded;
        // Called when the ad is closed.
        rewardBasedVideo.OnAdClosed += HandleRewardBasedVideoClosed;
        // Called when the ad click caused the user to leave the application.
        rewardBasedVideo.OnAdLeavingApplication += HandleRewardBasedVideoLeftApplication;
    }



    //Load RewardVideo
    private void RequestRewardBasedVideo()
    {



        // Create an empty ad request.
        AdRequest request = new AdRequest.Builder()
                .Build();
        // Load the rewarded video ad with the request.
        this.rewardBasedVideo.LoadAd(request, rewardAdUnitId);

    }


    //Show RewardVideo
    private void ShowRewardBasedVideo()
    {
        if (rewardBasedVideo.IsLoaded())
        {
            rewardBasedVideo.Show();
        }
    }


    //Load Interstitial
    private void RequestInterstitial()
    {


        // Initialize an InterstitialAd.
        this.interstitial = new InterstitialAd(interstitialAdUnitId);

        // Called when an ad request has successfully loaded.
        this.interstitial.OnAdLoaded += HandleOnAdLoaded;
        // Called when an ad request failed to load.
        this.interstitial.OnAdFailedToLoad += HandleOnAdFailedToLoad;
        // Called when an ad is shown.
        this.interstitial.OnAdOpening += HandleOnAdOpened;
        // Called when the ad is closed.
        this.interstitial.OnAdClosed += HandleOnAdClosed;
        // Called when the ad click caused the user to leave the application.
        this.interstitial.OnAdLeavingApplication += HandleOnAdLeavingApplication;


    }



    //Show Interstitial
    private void ShowInterstitial()
    {
        if (this.interstitial.IsLoaded())
        {
            this.interstitial.Show();
        }
    }

    // Reward callback method
    public void HandleRewardBasedVideoLoaded(object sender, EventArgs args)
    {
        MonoBehaviour.print("HandleRewardBasedVideoLoaded event received");
    }

    public void HandleRewardBasedVideoFailedToLoad(object sender, AdFailedToLoadEventArgs args)
    {
        MonoBehaviour.print(
            "HandleRewardBasedVideoFailedToLoad event received with message: "
                             + args.Message);
    }

    public void HandleRewardBasedVideoOpened(object sender, EventArgs args)
    {
        MonoBehaviour.print("HandleRewardBasedVideoOpened event received");
    }

    public void HandleRewardBasedVideoStarted(object sender, EventArgs args)
    {
        MonoBehaviour.print("HandleRewardBasedVideoStarted event received");
    }

    public void HandleRewardBasedVideoClosed(object sender, EventArgs args)
    {
        MonoBehaviour.print("HandleRewardBasedVideoClosed event received");
    }

    public void HandleRewardBasedVideoRewarded(object sender, Reward args)
    {
        string type = args.Type;
        double amount = args.Amount;
        MonoBehaviour.print(
            "HandleRewardBasedVideoRewarded event received for "
                        + amount.ToString() + " " + type);
    }

    public void HandleRewardBasedVideoLeftApplication(object sender, EventArgs args)
    {
        MonoBehaviour.print("HandleRewardBasedVideoLeftApplication event received");
    }



    //Interstitial callback methods
    public void HandleOnAdLoaded(object sender, EventArgs args)
    {
        MonoBehaviour.print("HandleAdLoaded event received");
    }

    public void HandleOnAdFailedToLoad(object sender, AdFailedToLoadEventArgs args)
    {
        MonoBehaviour.print("HandleFailedToReceiveAd event received with message: "
                            + args.Message);
    }

    public void HandleOnAdOpened(object sender, EventArgs args)
    {
        MonoBehaviour.print("HandleAdOpened event received");
    }

    public void HandleOnAdClosed(object sender, EventArgs args)
    {
        MonoBehaviour.print("HandleAdClosed event received");
    }

    public void HandleOnAdLeavingApplication(object sender, EventArgs args)
    {
        MonoBehaviour.print("HandleAdLeavingApplication event received");
    }
}

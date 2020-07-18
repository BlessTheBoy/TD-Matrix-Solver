using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.Gms.Ads;
using System.Threading.Tasks;
using Xamarin.Forms;


[assembly: Dependency(typeof(App1.Droid.InterstitialAdsImplementation))]
namespace App1.Droid
{
    public class InterstitialAdsImplementation : IAdmobInterstitialAds
    {
        public Task Display(string adId)
        {
            var displayTask = new TaskCompletionSource<bool>();
            InterstitialAd AdInterstitial = new InterstitialAd(Android.App.Application.Context)
            {
                AdUnitId = adId
            };
            {
                var adInterstitialListener = new AdInterstitialListener(AdInterstitial)
                {
                    AdClosed = () =>
                    {
                        if (displayTask != null)
                        {
                            displayTask.TrySetResult(AdInterstitial.IsLoaded);
                            displayTask = null;
                        }
                    },
                    AdFailed = () =>
                    {
                        if (displayTask != null)
                        {
                            displayTask.TrySetResult(AdInterstitial.IsLoaded);
                            displayTask = null;
                        }
                    }
                };

                AdRequest.Builder requestBuilder = new AdRequest.Builder();
                AdInterstitial.AdListener = adInterstitialListener;
                AdInterstitial.LoadAd(requestBuilder.Build());
            }

            return Task.WhenAll(displayTask.Task);
        }
    }

    public class AdInterstitialListener : AdListener
    {
        private readonly InterstitialAd _interstitialAd;

        public AdInterstitialListener(InterstitialAd interstitialAd)
        {
            _interstitialAd = interstitialAd;
        }

        public Action AdLoaded { get; set; }
        public Action AdClosed { get; set; }
        public Action AdFailed { get; set; }

        public override void OnAdLoaded()
        {
            base.OnAdLoaded();

            if (_interstitialAd.IsLoaded)
            {
                _interstitialAd.Show();
            }
            AdLoaded?.Invoke();
        }

        public override void OnAdClosed()
        {
            base.OnAdClosed();
            AdClosed?.Invoke();
        }

        public override void OnAdFailedToLoad(int errorCode)
        {
            base.OnAdFailedToLoad(errorCode);
            AdFailed?.Invoke();
        }
    }
}
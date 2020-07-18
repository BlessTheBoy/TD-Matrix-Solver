using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace App1
{
    public class AppConstants
    {
        public static string AppId
        {
            get
            {
                switch (Device.RuntimePlatform)
                {
                    case Device.Android:
                        return "";
                    default:
                        return "";
                }
            }
        }

        
        /// <summary>
        /// These Ids are test Ids from https://developers.google.com/admob/android/test-ads
        /// </summary>
        /// <value>The Interstitial Ad identifier.</value>
        public static string InterstitialAdId
        {
            get
            {
                switch (Device.RuntimePlatform)
                {
                    case Device.Android:
                        return "ca-app-pub-3940256099942544/1033173712";
                    default:
                        return "ca-app-pub-3940256099942544/1033173712";
                }
            }
        }        
    }
}

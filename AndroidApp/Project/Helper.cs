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

namespace AndroidApp.Project
{
   public static class Helper
    {
        /// <summary>
        /// Fast way to detect the OS version with names instead of just int
        /// To make my life easier.
        /// </summary>
        /// <returns></returns>
        public static string GetAndroidVersion()
        {
            switch (Xamarin.Essentials.DeviceInfo.Version.Major)
            {
                case 5:
                    {
                        return $"Lollipop {Xamarin.Essentials.DeviceInfo.Version}";
                    }

                case 6:
                    {
                        return $"Marshmallow {Xamarin.Essentials.DeviceInfo.Version}";
                    }

                case 7:
                    {
                        return $"Nougat {Xamarin.Essentials.DeviceInfo.Version}";
                    }

                case 8:
                    {
                        return $"Oreo {Xamarin.Essentials.DeviceInfo.Version}";
                    }

                case 9:
                    {
                        return $"Pie {Xamarin.Essentials.DeviceInfo.Version}";
                    }

                case 10:
                    {
                        return $"Android {Xamarin.Essentials.DeviceInfo.Version}";
                    }

                default:
                    {
                        return Xamarin.Essentials.DeviceInfo.Version.ToString();
                    }
            }
        }
    }
}
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

namespace ClientAndroid.Project
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
            switch ((int)Build.VERSION.SdkInt)
            {
                case 21:
                    {
                        return $"Lollipop 5.0";
                    }

                case 22:
                    {
                        return $"Lollipop 5.1";
                    }

                case 23:
                    {
                        return $"Marshmallow 6.0";
                    }

                case 24:
                    {
                        return $"Nougat 7.0";
                    }

                case 25:
                    {
                        return $"Nougat 7.1";
                    }

                case 26:
                    {
                        return $"Oreo 8.1";
                    }

                case 27:
                    {
                        return $"Oreo 8.0";
                    }

                case 28:
                    {
                        return $"Pie 9";
                    }

                case 29:
                    {
                        return $"Android 10";
                    }

                default:
                    {
                        return Build.VERSION.Release;
                    }
            }
        }
    }
}
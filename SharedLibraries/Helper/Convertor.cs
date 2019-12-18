using System;

namespace SharedLibraries.Helper
{
    public static class Convertor
    {
        public static string IntegerToUnitData(long length)
        {
            string[] suf = { "B", "KB", "MB", "GB", "TB", "PB", "EB" };
            if (length == 0)
                return "0" + suf[0];
            long bytes = Math.Abs(length);
            int place = Convert.ToInt32(Math.Floor(Math.Log(bytes, 1024)));
            double num = Math.Round(bytes / Math.Pow(1024, place), 1);
            return (Math.Sign(length) * num).ToString() + suf[place];
        }
    }
}

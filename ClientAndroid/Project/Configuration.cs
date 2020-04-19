using System;

namespace ClientAndroid.Project
{
    public static class Configuration
    {
        public static string Host = "192.168.1.233";
        public static int Port = 5505;
        public static readonly string Id = Guid.NewGuid().ToString();
    }
}
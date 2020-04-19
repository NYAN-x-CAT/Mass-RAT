using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ClientWindows.Settings
{
   public static class Configuration
    {
        public static string Host = "127.0.0.1";
        public static int Port = 5505;
        public static readonly string Id = Guid.NewGuid().ToString();
    }
}

using ClientWindows.Networking;
using System.Threading;

namespace ClientWindows
{
    /* 
           │ Author       : NYAN CAT
           │ Name         : Simple C# Tcp Socket Example
           │ Contact Me   : github.com/NYAN-x-CAT

           This program is distributed for educational purposes only.
    */

    class Program
    {
        static void Main()
        {
            new Thread(delegate ()
            {
                ClientSocket.ReceiveHeader();
            }).Start();
        }
    }
}

using API;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace App
{
    internal class Program
    {
        static AudioSource src;
        static void Main(string[] args)
        {
            Application.Init("Test App");
            Debug.LogInfo("Test Info Message.");
            Debug.LogWarning(Resources.Load<string>("HELLO_WORLD"));
            Application.Run("google.com");
#if DEBUG_BUILD
            Debug.LogInfo("DEBUG!!");
#endif
#if RELEASE_BUILD
            Debug.LogInfo("RELEASE!!");
#endif
            Console.ReadKey();
        }
    }
}

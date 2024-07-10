using API;
using API.GameEngine;
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
        static void Main(string[] args)
        {
            Application.Init("Test App");
            Debug.LogInfo("Test Info Message.");
            Debug.LogWarning(Resources.Load<string>("HELLO_WORLD"));
            //Application.Run("google.com");
#if DEBUG_BUILD
            Debug.LogInfo("DEBUG!!");
#endif
#if RELEASE_BUILD
            Debug.LogInfo("RELEASE!!");
#endif

            Game game = new Game();
            GameObject obj = new GameObject("Test GameObject");
            obj.AddComponent<Text>().text = "HELLO WORLD";
            obj.AddComponent<TestClassMove>();
            game.Run();
            Console.ReadKey();
        }
    }
}

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
        static AudioSource src;
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
            obj.position = new Vector2(0, 0);
            Debug.Log("X: " + obj.position.x + " Y: " + obj.position.y);
            Console.SetCursorPosition((int)obj.realPosition.x, (int)obj.realPosition.y);
            Console.Write("0");
            obj.position = new Vector2(0, 1);
            Debug.Log("X: " + obj.position.x + " Y: " + obj.position.y);
            Console.SetCursorPosition((int)obj.realPosition.x, (int)obj.realPosition.y);
            Console.Write("1");
            Console.ReadKey();
        }
    }
}

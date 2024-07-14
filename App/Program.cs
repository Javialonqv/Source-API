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
            GameObject obj = new GameObject("test");
            GameObject obj2 = new GameObject("test");
            obj.AddComponent<Text>().bgColor = ConsoleColor.Red;
            obj2.AddComponent<Text>().bgColor = ConsoleColor.Blue;
            obj.GetComponent<Text>().text = "HELLO WORLD!";
            obj2.GetComponent<Text>().text = "HOLA MUNDO!";
            obj2.position = new Vector2(0, 2);
            game.Run();
            Console.ReadKey();
        }
    }
}

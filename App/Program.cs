using API;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using API.GameEngine;
using API.GameEngine.Components;

namespace App
{
    internal class Program : MainBrain
    {
        GameObject pauseObj = null;
        static void Main(string[] args)
        {
            Application.Init("Test App");
            Debug.LogInfo("Test Message.");
            Debug.LogWarning(Resources.Load<string>("HELLO_WORLD"));
            //Application.Run("google.com");
            //Console.ReadKey();

            Game game = new Game();
            Program program = new Program();
            game.Run();
        }

        public override void Start()
        {
            pauseObj = new GameObject("TEST OBJ");
            pauseObj.AddComponent<Text>().text = "RESUMED";
        }

        bool paused = false;
        public override void Update()
        {
            if (Input.GetKeyDown(ConsoleKey.Escape))
            {
                paused = !paused;
            }

            pauseObj.GetComponent<Text>().text = paused ? "PAUSED" : "RESUMED";
        }
    }
}

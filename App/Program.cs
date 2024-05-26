using API;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using API.GameEngine;

namespace App
{
    internal class Program : MainBrain
    {
        static void Main(string[] args)
        {
            Application.Init("Test App");
            Debug.LogInfo("Test Message.");
            Debug.LogWarning(Resources.Load<string>("HELLO_WORLD"));
            Application.Run("google.com");
            Console.ReadKey();
        }
    }
}

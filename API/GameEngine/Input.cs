using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace API.GameEngine
{
    public static class Input
    {
        static ConsoleKeyInfo currentKey;
        static ConsoleKeyInfo previousKey;
        static bool isKeyPressed = false;
        static bool isKeyDown = false;
        static bool isKeyUp = false;

        internal static void Update()
        {
            bool avaiable = false;
            if ((avaiable = Console.KeyAvailable))
            {
                currentKey = Console.ReadKey(true);

                isKeyPressed = true;
                isKeyDown = currentKey.Key != previousKey.Key;
                isKeyUp = false;

                previousKey = currentKey;
            }
            else
            {
                isKeyPressed = false;
                isKeyDown = false;
                isKeyUp = previousKey.Key != 0;
                currentKey = new ConsoleKeyInfo();
                previousKey = new ConsoleKeyInfo();
            }
            Debug.Log("Current: " + currentKey.Key + " Previous one: " + previousKey.Key + " Key Avaiable: " + avaiable);
        }

        public static bool GetKeyDown(ConsoleKey consoleKey)
        {
            return currentKey.Key == consoleKey && isKeyDown;
        }
        public static bool GetKey(ConsoleKey consoleKey)
        {
            return currentKey.Key == consoleKey && isKeyPressed;
        }
        public static bool GetKeyUp(ConsoleKey consoleKey)
        {
            return currentKey.Key == consoleKey && isKeyUp;
        }
    }
}

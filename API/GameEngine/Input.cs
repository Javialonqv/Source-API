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
            if (Console.KeyAvailable)
            {
                currentKey = Console.ReadKey(true);

                isKeyDown = currentKey.Key != previousKey.Key;
                isKeyUp = false;

                isKeyPressed = true;

                previousKey = currentKey;
            }
            else
            {
                isKeyDown = false;
                isKeyUp = previousKey.Key != 0;
                isKeyPressed = false;
                previousKey = new ConsoleKeyInfo();
            }
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

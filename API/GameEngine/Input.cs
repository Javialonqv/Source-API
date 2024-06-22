using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace API.GameEngine
{
    public static class Input
    {
        static HashSet<ConsoleKey> currentKeys = new HashSet<ConsoleKey>();
        static HashSet<ConsoleKey> previousKeys = new HashSet<ConsoleKey>();

        internal static void Update()
        {
            previousKeys = new HashSet<ConsoleKey>(currentKeys);
            currentKeys.Clear();

            while (Console.KeyAvailable)
            {
                var key = Console.ReadKey(true).Key;
                currentKeys.Add(key);
            }
        }

        public static bool GetKey(ConsoleKey consoleKey)
        {
            return currentKeys.Contains(consoleKey);
        }
        public static bool GetKeyDown(ConsoleKey consoleKey)
        {
            return currentKeys.Contains(consoleKey) && !previousKeys.Contains(consoleKey);
        }
        public static bool GetKeyUp(ConsoleKey consoleKey)
        {
            return !currentKeys.Contains(consoleKey) && previousKeys.Contains(consoleKey);
        }
    }
}

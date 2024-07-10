using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace API.GameEngine
{
    public static class Input
    {
        static HashSet<ConsoleKey> currentKeys = new HashSet<ConsoleKey>();
        static HashSet<ConsoleKey> previousKeys = new HashSet<ConsoleKey>();

        [DllImport("user32.dll")]
        static extern short GetAsyncKeyState(int vKey);

        internal static void Update()
        {
            previousKeys = new HashSet<ConsoleKey>(currentKeys);
            currentKeys.Clear();

            for (int i = 0; i < 256; i++)
            {
                if (GetAsyncKeyState(i) < 0) { currentKeys.Add((ConsoleKey)i); }
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

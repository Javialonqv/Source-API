using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace API.GameEngine
{
    /// <summary>
    /// Main class for the managment of the Input system.
    /// </summary>
    public static class Input
    {
        /// <summary>
        /// Specifies the current pressed keys on this frame.
        /// </summary>
        static HashSet<ConsoleKey> currentKeys = new HashSet<ConsoleKey>();
        /// <summary>
        /// Specifies the previous pressed keys on the previous frame.
        /// </summary>
        static HashSet<ConsoleKey> previousKeys = new HashSet<ConsoleKey>();
        /// <summary>
        /// Specifies if any key is pressed on the current frame.
        /// </summary>
        public static bool anyKey
        {
            get
            {
                return currentKeys.Count > 0;
            }
        }

        /// <summary>
        /// Used for get the current state of the specified key using the user32.dll.
        /// </summary>
        /// <param name="vKey"></param>
        /// <returns></returns>
        [DllImport("user32.dll")]
        static extern short GetAsyncKeyState(int vKey);

        /// <summary>
        /// This method is called every frame by an external script.
        /// </summary>
        internal static void Update()
        {
            previousKeys = new HashSet<ConsoleKey>(currentKeys);
            currentKeys.Clear();

            // Check for all the keys and check if they are pressed:
            for (int i = 0; i < 256; i++)
            {
                if (GetAsyncKeyState(i) < 0) { currentKeys.Add((ConsoleKey)i); }
            }
        }

        /// <summary>
        /// Returns true while the specified key is pressed.
        /// </summary>
        /// <param name="consoleKey">The key to check if pressed.</param>
        /// <returns></returns>
        public static bool GetKey(ConsoleKey consoleKey)
        {
            return currentKeys.Contains(consoleKey);
        }
        /// <summary>
        /// Returns true during the frame the user is pressing the specified key.
        /// </summary>
        /// <param name="consoleKey">The key to check if pressed.</param>
        /// <returns></returns>
        public static bool GetKeyDown(ConsoleKey consoleKey)
        {
            return currentKeys.Contains(consoleKey) && !previousKeys.Contains(consoleKey);
        }
        /// <summary>
        /// Returns true during the frame the user is releasing the specified key.
        /// </summary>
        /// <param name="consoleKey">The key to check if released.</param>
        /// <returns></returns>
        public static bool GetKeyUp(ConsoleKey consoleKey)
        {
            return !currentKeys.Contains(consoleKey) && previousKeys.Contains(consoleKey);
        }

        /// <summary>
        /// Returns true while the specified button identified as buttonName is pressed.
        /// </summary>
        /// <param name="buttonName">The name of the button.</param>
        /// <returns></returns>
        public static bool GetButton(string buttonName)
        {
            if (ConfigFile.loaded.buttons.ContainsKey(buttonName))
            {
                List<ConsoleKey> keys = new List<ConsoleKey>();
                foreach (string key in ConfigFile.loaded.buttons[buttonName])
                {
                    if (Enum.TryParse(key, true, out ConsoleKey consoleKey)) { keys.Add(consoleKey); }
                }
                foreach (ConsoleKey consoleKey in keys)
                {
                    if (currentKeys.Contains(consoleKey)) { return true; }
                }
            }
            return false;
        }
        /// <summary>
        /// Returns true during the frame the user is pressing the specified button identified as buttonName.
        /// </summary>
        /// <param name="buttonName">The name of the button.</param>
        /// <returns></returns>
        public static bool GetButtonDown(string buttonName)
        {
            if (ConfigFile.loaded.buttons.ContainsKey(buttonName))
            {
                List<ConsoleKey> keys = new List<ConsoleKey>();
                foreach (string key in ConfigFile.loaded.buttons[buttonName])
                {
                    if (Enum.TryParse(key, true, out ConsoleKey consoleKey)) { keys.Add(consoleKey); }
                }
                foreach (ConsoleKey consoleKey in keys)
                {
                    if (currentKeys.Contains(consoleKey) && !previousKeys.Contains(consoleKey)) { return true; }
                }
            }
            return false;
        }
        /// <summary>
        /// Returns true during the frame the user is releasing the specified button identified as buttonName.
        /// </summary>
        /// <param name="buttonName">The name of the button.</param>
        /// <returns></returns>
        public static bool GetButtonUp(string buttonName)
        {
            if (ConfigFile.loaded.buttons.ContainsKey(buttonName))
            {
                List<ConsoleKey> keys = new List<ConsoleKey>();
                foreach (string key in ConfigFile.loaded.buttons[buttonName])
                {
                    if (Enum.TryParse(key, true, out ConsoleKey consoleKey)) { keys.Add(consoleKey); }
                }
                foreach (ConsoleKey consoleKey in keys)
                {
                    if (!currentKeys.Contains(consoleKey) && previousKeys.Contains(consoleKey)) { return true; }
                }
            }
            return false;
        }
    }
}

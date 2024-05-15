using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace API
{
    /// <summary>
    /// Toolkit for the general use of the Source API.
    /// </summary>
    public static class Application
    {
        /// <summary>
        /// Represents if the Source API has been initialized.
        /// </summary>
        internal static bool initialized = false;

        /// <summary>
        /// Initialize the Source API. Needs to be called at the start of the runtime.
        /// </summary>
        /// <param name="windowTitle">Specifies the tittle of the window. This one can't be empty of null.</param>
        public static void Init(string windowTitle)
        {
            if (initialized) { return; } // This method can only be called once.
            Console.WriteLine("Initializing Engine...");
            AppDomain.CurrentDomain.ProcessExit += (sender, args) => Debug.KillLogger(); // Kills the logger on exit.
            initialized = true;
            Console.WriteLine("Initializing Paths...");
            Paths.Init(false); // Inits the Paths class.
            Console.WriteLine("Reading Source Config File...");
            ConfigFile.Init(Paths.sourceConfigFilePath); // Inits the ConfigFile class.
            Console.WriteLine("Setting up Window Configurations...");
            Window.Init(Window.MAIN_WIDTH, Window.MAIN_HEIGHT, windowTitle); // Inits the Window class.
            if (ConfigFile.LoggerEnabled)
            {
                Console.WriteLine("Initializing Logger...");
                Debug.Init(true); // Inits the Debug class ONLY if the logger is enabled on the JSON file.
            }
            Console.WriteLine("Initializing Resources...");
            Resources.Init(false); // Inits the Resources class.
            Console.WriteLine("FINISHED SETUP!!!");
            Console.Clear();
        }
    }
}

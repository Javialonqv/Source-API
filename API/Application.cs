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
        /// Specifies if the Source API has been initialized.
        /// </summary>
        internal static bool initialized = false;
        /// <summary>
        /// Points to the data path of the app.
        /// </summary>
        public static string dataPath
        {
            get
            {
                if (Paths.initialized) { return Paths.dataFolderPath; }
                else { return ""; }
            }
        }

        /// <summary>
        /// Initialize the Source API. Needs to be called at the start of the runtime.
        /// </summary>
        public static void Init()
        {
            if (initialized) { return; } // This method can only be called once.
            Console.WriteLine("Initializing Engine...");
            AppDomain.CurrentDomain.ProcessExit += (sender, args) => Debug.KillLogger(); // Kills the logger on exit.
            initialized = true;
            Console.WriteLine("Initializing Paths...");
            Paths.Init(false, ""); // Inits the Paths class.
            Console.WriteLine("Reading Source Config File...");
            ConfigFile.Init(false, Paths.sourceConfigFilePath); // Inits the ConfigFile class.
            Console.WriteLine("Setting up Window Configurations...");
            Window.Init(Window.MAIN_WIDTH, Window.MAIN_HEIGHT, ""); // Inits the Window class.
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
        /// <summary>
        /// Initialize the Source API. Needs to be called at the start of the runtime.
        /// </summary>
        /// <param name="windowTitle">Specifies the tittle of the window. This one can't be empty of null.</param>
        public static void Init(string windowTitle)
        {
            if (!initialized)
            {
                Console.WriteLine("Initializing Engine...");
                AppDomain.CurrentDomain.ProcessExit += (sender, args) => Debug.KillLogger(); // Kills the logger on exit.
                initialized = true;
                Console.WriteLine("Initializing Paths...");
                Paths.Init(false, ""); // Inits the Paths class.
                Console.WriteLine("Reading Source Config File...");
                ConfigFile.Init(false, Paths.sourceConfigFilePath); // Inits the ConfigFile class.
                Console.WriteLine("Setting up Window Configurations...");
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
            Window.Init(Window.MAIN_WIDTH, Window.MAIN_HEIGHT, windowTitle); // Inits the Window class.
        }

        /// <summary>
        /// Initialize the Source API. Only needs to be called via the Source Launcher.
        /// </summary>
        internal static void InitWithSRCFile(string srcFilePath)
        {
            Console.WriteLine("Initializing Engine...");
            AppDomain.CurrentDomain.ProcessExit += (sender, args) => Debug.KillLogger(); // Kills the logger on exit.
            initialized = true;
            Console.WriteLine("Initializing Paths...");
            Paths.Init(true, srcFilePath); // Inits the Paths class.
            Console.WriteLine("Reading Source Config File...");
            ConfigFile.Init(true, Paths.sourceConfigFilePath); // Inits the ConfigFile class.
            Console.WriteLine("Setting up Window Configurations...");
            Window.Init(Window.MAIN_WIDTH, Window.MAIN_HEIGHT, ""); // Inits the Window class.
            if (ConfigFile.LoggerEnabled)
            {
                Console.WriteLine("Initializing Logger...");
                Debug.Init(true); // Inits the Debug class ONLY if the logger is enabled on the JSON file.
            }
            Console.WriteLine("Initializing Resources...");
            Resources.Init(true); // Inits the Resources class.
            Console.WriteLine("FINISHED SETUP!!!");
            Console.Clear();
        }

        /// <summary>
        /// Stops the code execution and exits of the app.
        /// </summary>
        public static void Quit()
        {
            Environment.Exit(0);
        }
        /// <summary>
        /// Stops the code execution and exits of the app.
        /// </summary>
        /// <param name="exitCode">The exit code result.</param>
        public static void Quit(int exitCode)
        {
            Environment.Exit(exitCode);
        }
    }
}

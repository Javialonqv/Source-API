using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace API
{
    public static class Application
    {
        internal static bool initialized = false;

        public static void Init(string windowTitle)
        {
            Console.WriteLine("Initializing Engine...");
            AppDomain.CurrentDomain.ProcessExit += (sender, args) => Debug.KillLogger();
            initialized = true;
            Console.WriteLine("Initializing Paths...");
            Paths.Init(false);
            Console.WriteLine("Reading Source Config File...");
            ConfigFile.Init(Paths.sourceConfigFilePath);
            Console.WriteLine("Setting up Window Configurations...");
            Window.Init(Window.MAIN_WIDTH, Window.MAIN_HEIGHT, windowTitle);
            if (ConfigFile.LoggerEnabled)
            {
                Console.WriteLine("Initializing Logger...");
                Debug.Init(true);
            }
            Console.WriteLine("Initializing Resources...");
            Resources.Init(false);
            Console.WriteLine("FINISHED SETUP!!!");
            Console.Clear();
        }
    }
}

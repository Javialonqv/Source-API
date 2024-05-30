using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using System.Runtime.InteropServices;
using System.Globalization;

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
        /// Points to a user folder specifically maded for this app. Perfect for storing USER data.
        /// </summary>
        public static string persistenDataPath
        {
            get
            {
                if (Paths.initialized)
                {
                    string path = Path.Combine(Paths.persistentDataPathParent, ConfigFile.loaded.packageName);
                    if (!Directory.Exists(path)) { Directory.CreateDirectory(path); }
                    return path;
                }
                else { return ""; }
            }
        }
        /// <summary>
        /// Specifies if the app is in a debug build.
        /// </summary>
        public static bool isDebugBuild
        {
            get { return Debug.isDebugBuild; }
        }
        /// <summary>
        /// Specifies the system language of the current user.
        /// </summary>
        public static string systemLanguage
        {
            get
            {
                return CultureInfo.CurrentCulture.TwoLetterISOLanguageName;
            }
        }

        #region Is Focus
        [DllImport("user32.dll")]
        static extern IntPtr GetForegroundWindow();
        [DllImport("user32.dll")]
        static extern int GetWindowText(IntPtr hWnd, StringBuilder text, int count);
        static string GetActiveWindowTitle()
        {
            const int nChars = 256;
            StringBuilder Buff = new StringBuilder(nChars);
            IntPtr handle = GetForegroundWindow();

            if (GetWindowText(handle, Buff, nChars) > 0)
            {
                return Buff.ToString();
            }
            return null;
        }
        /// <summary>
        /// Specifies if this app is the active window.
        /// </summary>
        public static bool isFocus
        {
            get { return GetActiveWindowTitle() == Window.title; }
        }
        #endregion

        /// <summary>
        /// Initialize the Source API. Needs to be called at the start of the runtime.
        /// </summary>
        public static void Init()
        {
            if (initialized) { return; } // This method can only be called once.
            Console.WriteLine("[*] Initializing Engine...");
            AppDomain.CurrentDomain.ProcessExit += (sender, args) => Debug.KillLogger(); // Kills the logger on exit.
            initialized = true;
            Console.WriteLine("[*] Initializing Paths...");
            Paths.Init(false, ""); // Inits the Paths class.
            Console.WriteLine("[*] Reading Source Config File...");
            ConfigFile.Init(false, Paths.sourceConfigFilePath); // Inits the ConfigFile class.
            Console.WriteLine("[*] Setting up Window Configurations...");
            Window.Init(Window.MAIN_WIDTH, Window.MAIN_HEIGHT, ""); // Inits the Window class.
            if (ConfigFile.LoggerEnabled)
            {
                Console.WriteLine("[*] Initializing Logger...");
                Debug.Init(true); // Inits the Debug class ONLY if the logger is enabled on the JSON file.
            }
            Console.WriteLine("[*] Initializing Resources...");
            Resources.Init(false); // Inits the Resources class.
            Console.WriteLine("[*] FINISHED SETUP!!!");
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
                Console.WriteLine("[*] Initializing Engine...");
                AppDomain.CurrentDomain.ProcessExit += (sender, args) => Debug.KillLogger(); // Kills the logger on exit.
                initialized = true;
                Console.WriteLine("[*] Initializing Paths...");
                Paths.Init(false, ""); // Inits the Paths class.
                Console.WriteLine("[*] Reading Source Config File...");
                ConfigFile.Init(false, Paths.sourceConfigFilePath); // Inits the ConfigFile class.
                Console.WriteLine("[*] Setting up Window Configurations...");
                if (ConfigFile.LoggerEnabled)
                {
                    Console.WriteLine("[*] Initializing Logger...");
                    Debug.Init(true); // Inits the Debug class ONLY if the logger is enabled on the JSON file.
                }
                Console.WriteLine("[*] Initializing Resources...");
                Resources.Init(false); // Inits the Resources class.
                Console.WriteLine("[*] FINISHED SETUP!!!");
                Console.Clear();
            }
            Window.Init(Window.MAIN_WIDTH, Window.MAIN_HEIGHT, windowTitle); // Inits the Window class.
        }

        /// <summary>
        /// Initialize the Source API. Only needs to be called via the Source Launcher.
        /// </summary>
        internal static void InitWithSRCFile(string srcFilePath)
        {
            Console.WriteLine("[*] Initializing Engine...");
            AppDomain.CurrentDomain.ProcessExit += (sender, args) => Debug.KillLogger(); // Kills the logger on exit.
            initialized = true;
            Debug.isDebugBuild = false;
            Console.WriteLine("[*] Initializing Paths...");
            Paths.Init(true, srcFilePath); // Inits the Paths class.
            Console.WriteLine("[*] Reading Source Config File...");
            ConfigFile.Init(true, Paths.sourceConfigFilePath); // Inits the ConfigFile class.
            Console.WriteLine("[*] Setting up Window Configurations...");
            Window.Init(Window.MAIN_WIDTH, Window.MAIN_HEIGHT, ""); // Inits the Window class.
            if (ConfigFile.LoggerEnabled)
            {
                Console.WriteLine("[*] Initializing Logger...");
                Debug.Init(true); // Inits the Debug class ONLY if the logger is enabled on the JSON file.
            }
            Console.WriteLine("[*] Initializing Resources...");
            Resources.Init(true); // Inits the Resources class.
            Console.WriteLine("[*] FINISHED SETUP!!!");
            Console.Clear();
        }

        /// <summary>
        /// Runs a local file in the machine or a online URL.
        /// </summary>
        /// <param name="url">The url to run.</param>
        public static void Run(string url)
        {
            // Check first if the specified url actually is a local file. In that case, open the file or directory.
            if (File.Exists(url) || Directory.Exists(url))
            {
                Process.Start(new ProcessStartInfo(url) { UseShellExecute = true });
                return;
            }

            // If the url seems to be a online URL and doesn't contains an scheme, add http by default.
            if (!url.Contains("://") && (url.Contains(".") || url.StartsWith("www.")))
            {
                url = "http://" + url;
            }
            // For some reason, Uri.IsWellFormedUriString returns false with a file://, so, manage it manually.
            if (url.StartsWith("file://"))
            {
                string localPath = new Uri(url).LocalPath;
                if (File.Exists(localPath) || Directory.Exists(localPath))
                {
                    Process.Start(new ProcessStartInfo(url) { UseShellExecute = true });
                    return;
                }
            }
            // If the URL is a valid one, and its http, https or file, open it in the default browser.
            if (Uri.IsWellFormedUriString(url, UriKind.Absolute))
            {
                Uri uri = new Uri(url);
                if (uri.Scheme == Uri.UriSchemeHttp || uri.Scheme == Uri.UriSchemeHttps || uri.Scheme == Uri.UriSchemeFile)
                {
                    Process.Start(new ProcessStartInfo(url) { UseShellExecute = true });
                    return;
                }
            }
            // If nothing apply, do nothing and throw an exception.
            ExceptionsManager.SpecifiedFileDoesntExists(url);
        }

        /// <summary>
        /// Stops the code execution and exits of the app.
        /// </summary>
        public static void Quit()
        {
            Debug.KillLogger();
            Environment.Exit(0);
        }
        /// <summary>
        /// Stops the code execution and exits of the app.
        /// </summary>
        /// <param name="exitCode">The exit code result.</param>
        public static void Quit(int exitCode)
        {
            Debug.KillLogger();
            Environment.Exit(exitCode);
        }
    }
}

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.IO.Pipes;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace API
{
    /// <summary>
    /// Toolkit for debugging the Source application.
    /// </summary>
    public static class Debug
    {
        /// <summary>
        /// Represents if the logger is enabled.
        /// </summary>
        static bool loggerEnabled = false;
        /// <summary>
        /// List of pending messages to send to the logger.
        /// </summary>
        static Queue<object> messages = new Queue<object>();

        /// <summary>
        /// Inits the debugger function.
        /// </summary>
        /// <param name="loggerEnabled">Represents if the logger is enabled.</param>
        internal static void Init(bool loggerEnabled)
        {
            Debug.loggerEnabled = loggerEnabled;
            if (loggerEnabled) // If logger is enabled, start the logger in another thread.
            {
                Process.Start(Paths.loggerExecutableFilePath);
                Thread thread = new Thread(new ThreadStart(InitializeClient));
                thread.IsBackground = true;
                thread.Start();
            }
        }

        static void InitializeClient()
        {
            // Unfortunely, there' no way to create two windows in one app, so, we need to make another app.
            // This use a Pipe Stream to send the info across the two apps.
            using (var client = new NamedPipeClientStream(".", "SourceLogger"))
            {
                client.Connect();
                using (var writer = new StreamWriter(client))
                {
                    writer.AutoFlush = true;
                    while (true)
                    {
                        while (messages.Count > 0)
                        {
                            writer.WriteLine(messages.Dequeue());
                        }
                    }
                }
            }
        }

        /// <summary>
        /// Logs an object if the logger it's active.
        /// </summary>
        /// <param name="obj">The object to print on the logger's console.</param>
        public static void Log(object obj)
        {
            if (!loggerEnabled) { ExceptionsManager.LoggerNOTEnabled(); return; }
            messages.Enqueue("[LOG] " + obj);
        }
        /// <summary>
        /// Logs an object as info if the logger it's active.
        /// </summary>
        /// <param name="obj">The object to print on the logger's console as info.</param>
        public static void LogInfo(object obj)
        {
            if (!loggerEnabled) { ExceptionsManager.LoggerNOTEnabled(); return; }
            messages.Enqueue("[INFO] " + obj);
        }
        /// <summary>
        /// Logs an object as a warning if the logger it's active.
        /// </summary>
        /// <param name="obj">The object to print on the logger's console as a warning.</param>
        public static void LogWarning(object obj)
        {
            if (!loggerEnabled) { ExceptionsManager.LoggerNOTEnabled(); return; }
            messages.Enqueue("[WARNING] " + obj);
        }
        /// <summary>
        /// Logs an object as an error if the logger it's active.
        /// </summary>
        /// <param name="obj">The object to print on the logger's console as an error.</param>
        public static void LogError(object obj)
        {
            if (!loggerEnabled) { ExceptionsManager.LoggerNOTEnabled(); return; }
            messages.Enqueue("[ERROR] " + obj);
        }
        /// <summary>
        /// Logs an object as an internal error if the logger it's active.
        /// </summary>
        /// <param name="obj">The object to print on the logger's console as an internal error.</param>
        internal static void LogInternalError(object obj)
        {
            if (!loggerEnabled) { ExceptionsManager.LoggerNOTEnabled(); return; }
            messages.Enqueue("[INTERNAL ERROR] " + obj);
        }
        /// <summary>
        /// Kills the logger if enabled.
        /// </summary>
        internal static void KillLogger()
        {
            // Since this method it's called on app close, remove the error line to avoid bugs.
            if (!loggerEnabled) { /*ExceptionsManager.LoggerNotEnabled();*/ return; }
            messages.Enqueue("kill-server");
        }
    }
}

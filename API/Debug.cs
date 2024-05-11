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
    public static class Debug
    {
        static Queue<object> messages = new Queue<object>();

        internal static void Init(bool loggerEnabled)
        {
            if (loggerEnabled)
            {
                Process.Start(Paths.loggerExecutableFilePath);
                Thread thread = new Thread(new ThreadStart(InitializeClient));
                thread.IsBackground = true;
                thread.Start();
            }
        }

        static void InitializeClient()
        {
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

        public static void Log(object obj)
        {
            //if (!loggerEnabled) { ExceptionsManager.LoggerNotEnabled(); return; }
            messages.Enqueue("[LOG] " + obj);
        }
        public static void LogInfo(object obj)
        {
            //if (!loggerEnabled) { ExceptionsManager.LoggerNotEnabled(); return; }
            messages.Enqueue("[INFO] " + obj);
        }
        public static void LogWarning(object obj)
        {
            //if (!loggerEnabled) { ExceptionsManager.LoggerNotEnabled(); return; }
            messages.Enqueue("[WARNING] " + obj);
        }
        public static void LogError(object obj)
        {
            //if (!loggerEnabled) { ExceptionsManager.LoggerNotEnabled(); return; }
            messages.Enqueue("[ERROR] " + obj);
        }
        internal static void KillLogger()
        {
            //if (!loggerEnabled) { ExceptionsManager.LoggerNotEnabled(); return; }
            messages.Enqueue("kill-server");
        }
    }
}

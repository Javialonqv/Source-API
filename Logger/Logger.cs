using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Pipes;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logger
{
    internal class Logger
    {
        static void Main(string[] args)
        {
            Console.Title = "Source Logger";
            Connection:
            using (var server = new NamedPipeServerStream("SourceLogger"))
            {
                Console.WriteLine("<Waiting for connection with the app...>");
                server.WaitForConnection();
                Console.Clear();

                using (var reader = new StreamReader(server))
                {
                    while (true)
                    {
                        if (!server.IsConnected)
                        {
                            server.Dispose();
                            Console.ForegroundColor = ConsoleColor.White;
                            Console.Clear();
                            goto Connection;
                        }
                        string line = reader.ReadLine();
                        if (!string.IsNullOrEmpty(line))
                        {
                            if (line == "kill-server") { Environment.Exit(0); }
                            if (line.StartsWith("[LOG]")) { Console.ForegroundColor = ConsoleColor.White; }
                            if (line.StartsWith("[INFO]")) { Console.ForegroundColor = ConsoleColor.Blue; }
                            if (line.StartsWith("[WARNING]")) { Console.ForegroundColor = ConsoleColor.Yellow; }
                            if (line.StartsWith("[ERROR]")) { Console.ForegroundColor = ConsoleColor.Red; }
                            Console.WriteLine(line);
                        }
                    }
                }
            }
        }
    }
}

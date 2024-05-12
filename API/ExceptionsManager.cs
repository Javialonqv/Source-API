using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace API
{
    internal static class ExceptionsManager
    {
        public static void CantFindSourceConfigFile(string filePath)
        {
            ShowError($"Can't find the SourceConfig.json file at \"{filePath}\", the default settings will be used.");
        }
        public static void CantDeserealizeSourceConfigFile(string filePath)
        {
            ShowError($"Can't deserealize the SourceConfig.json file at \"{filePath}\" maybe NOT a JSON file.");
        }
        public static void CantFindLoggerExecutableFile(string filePath)
        {
            ShowError($"Can't find the Logger.exe file at \"{filePath}\".");
        }
        public static void CantFindContentFolder(string folderPath)
        {
            ShowError($"Can't find the \"Content\" folder at \"{folderPath}\".");
        }
        public static void ErrorResizingWindow(Exception e)
        {
            ShowError($"Error resizing console window: \"{e.Message}\".");
        }
        public static void TheConsoleTitleCantBeEmpty(string windowTitle)
        {
            ShowError($"The title of the console window \"{windowTitle}\" can't be empty");
        }
        public static void LoggerNOTEnabled()
        {
            ShowError($"The logger isn't enabled, can't send messages.");
        }
        public static void CantFindTheSpecifiedResource(string resourceName)
        {
            ShowError($"Can't find the \"{resourceName}\" file INSIDE of the \"Content\" folder.");
        }

        static void ShowError(object errorMessage)
        {
            if (ConfigFile.LoggerEnabled)
            {
                Debug.LogInternalError(errorMessage);
            }
            else
            {
                MessageBox.Show(errorMessage.ToString(), "INTERNAL ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}

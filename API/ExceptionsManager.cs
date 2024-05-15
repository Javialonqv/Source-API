using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace API
{
    /// <summary>
    /// Tookit to manage some main exceptions that can occur during runtime.
    /// </summary>
    internal static class ExceptionsManager
    {
        /// <summary>
        /// Prints an error that says the SourceConfig.json file can't be found.
        /// </summary>
        /// <param name="filePath">The path of the SourceConfig.json file.</param>
        public static void CantFindSourceConfigFile(string filePath)
        {
            ShowError($"Can't find the SourceConfig.json file at \"{filePath}\", the default settings will be used.");
        }
        /// <summary>
        /// Prints an error that says the SourceConfig.json file can't be deserealized.
        /// </summary>
        /// <param name="filePath">The path of the SourceConfig.json file.</param>
        public static void CantDeserealizeSourceConfigFile(string filePath)
        {
            ShowError($"Can't deserealize the SourceConfig.json file at \"{filePath}\" maybe NOT a JSON file.");
        }
        /// <summary>
        /// Prints an error that says the Logger.exe file can't be found.
        /// </summary>
        /// <param name="filePath">The path of the Logger.exe file.</param>
        public static void CantFindLoggerExecutableFile(string filePath)
        {
            ShowError($"Can't find the Logger.exe file at \"{filePath}\".");
        }
        /// <summary>
        /// Prints an error that says the "Content" folder can't be found.
        /// </summary>
        /// <param name="folderPath">The path of the "Content" folder.</param>
        public static void CantFindContentFolder(string folderPath)
        {
            ShowError($"Can't find the \"Content\" folder at \"{folderPath}\".");
        }
        /// <summary>
        /// Prints an error that says the window can't be resized for some reason.
        /// </summary>
        /// <param name="e">The exception produced when the error ocurred.</param>
        public static void ErrorResizingWindow(Exception e)
        {
            ShowError($"Error resizing console window: \"{e.Message}\".");
        }
        /// <summary>
        /// Prints an error that says the console's title can't be empty or null.
        /// </summary>
        /// <param name="windowTitle">The console's title trying to put in.</param>
        public static void TheConsoleTitleCantBeEmpty(string windowTitle)
        {
            ShowError($"The title of the console window \"{windowTitle}\" can't be empty");
        }
        /// <summary>
        /// Prints an error that says the logger isn't enabled and can't send messages to it.
        /// </summary>
        public static void LoggerNOTEnabled()
        {
            ShowError($"The logger isn't enabled, can't send messages.");
        }
        /// <summary>
        /// Prints an error that says the specified resource of the "Content" folder can't be found.
        /// </summary>
        /// <param name="resourceName">The name of the resource.</param>
        public static void CantFindTheSpecifiedResource(string resourceName)
        {
            ShowError($"Can't find the \"{resourceName}\" file INSIDE of the \"Content\" folder.");
        }

        /// <summary>
        /// Shows the specified error into a dialog box or logger if it's enabled.
        /// </summary>
        /// <param name="errorMessage">The error to print.</param>
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

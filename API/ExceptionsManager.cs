using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
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
        /// Prints an error that says the specified resource can't be deserealized as the specified type.
        /// </summary>
        /// <param name="resourceName">The name of the resource to deserealize.</param>
        /// <param name="type">The type trying to convert to.</param>
        public static void ErrorDeserealizingSpecifiedResources(string resourceName, Type type)
        {
            ShowError($"Error deserealizing the \"{resourceName}\" as \"{type.FullName}\".");
        }
        /// <summary>
        /// Prints an error that says the object of the specified type can't be serialized.
        /// </summary>
        /// <param name="objectType">The type of the object trying to serialize.</param>
        public static void CantSerializeJsonFile(Type objectType)
        {
            ShowError($"Can't serialize the object of type {objectType.FullName}. Mayble NOT a JSON file.");
        }
        /// <summary>
        /// Prints an error that says a JSON can't be deserealized as the specified type.
        /// </summary>
        /// <param name="objectType">The type of the object trying to deserialize to.</param>
        public static void CantDeserializeJsonFile(Type objectType)
        {
            ShowError($"Can't deserialize a JSON as the {objectType.FullName} type.");
        }
        /// <summary>
        /// Prints an error that says the specified file can't be found in the current machine. Maybe trying to call Application.Run().
        /// </summary>
        /// <param name="filePath">The path of the specified file.</param>
        public static void SpecifiedFileDoesntExists(string filePath)
        {
            ShowError($"Can't find the \"{filePath}\" file in the current machine.");
        }
        /// <summary>
        /// Prints an error that says the specified audio file can't be loaded.
        /// </summary>
        /// <param name="filePath"></param>
        public static void ErrorLoadingAudioFile(string filePath)
        {
            ShowError($"Can't load the audio file at \"{filePath}\".");
        }
        /// <summary>
        /// Prints an error that says a Game class instance is already created in the runtime.
        /// </summary>
        public static void GameInstanceAlreadyCreated()
        {
            ShowError("A Game class instance has already been initialized.");
        }
        /// <summary>
        /// Prints an error that says the Game class hasn't been initialized yet.
        /// </summary>
        public static void GameClassNotInitializedYet()
        {
            ShowError($"The Game class hasn't been initialized.");
        }
        /// <summary>
        /// Prints an error that says the specified component in the GameObject wasn't found.
        /// </summary>
        /// <param name="gameObjectName">The name of the GameObject.</param>
        public static void CantFindSpecifiedComponent(string gameObjectName)
        {
            ShowError($"Can't find the specified component in the gameobject of name: \"{gameObjectName}\"");
        }
        /// <summary>
        /// Prints an error that says the specified index was outside of the bounds of the child count.
        /// </summary>
        /// <param name="childCount">The total number of childs of the object.</param>
        /// <param name="specifiedIndex">The specified index number.</param>
        public static void IndexOutsideOfBoundsOfTheNumberOfChilds(int childCount, int specifiedIndex)
        {
            ShowError($"Index \"{specifiedIndex}\" was outside of the bounds of the object, the number of childs is \"{childCount}\".");
        }
        public static void TypeDoesntInheritFromComponent(string typeName)
        {
            ShowError($"The type of \"{typeName}\" doesn't inherit from the Component class.");
        }

        /// <summary>
        /// Shows the specified error into a dialog box or logger if it's enabled.
        /// </summary>
        /// <param name="errorMessage">The error to print.</param>
        static void ShowError(object errorMessage)
        {
            if (ConfigFile.LoggerEnabled)
            {
                Debug.LogInternalError(errorMessage.ToString() + GetCallStack());
            }
            else
            {
                MessageBox.Show(errorMessage.ToString() + GetCallStack(), "INTERNAL ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        static string GetCallStack()
        {
            /*StackTrace trace = new StackTrace();

            if (trace.FrameCount > 2)
            {
                StackFrame frame = trace.GetFrame(3);
                MethodBase method = frame.GetMethod();
                return $"{method.DeclaringType.FullName}.{method.Name}";
            }
            else
            {
                return "UNDEFINED";
            }*/

            StackTrace trace = new StackTrace(true);
            StringBuilder sb = new StringBuilder();

            for (int i = 3; i < trace.FrameCount; i++)
            {
                StackFrame frame = trace.GetFrame(i);
                MethodBase method = frame.GetMethod();

                string className = method.DeclaringType.FullName;
                string methodName = method.Name;

                sb.AppendLine($" at {className}.{methodName}");
            }
            return sb.ToString();
        }
    }
}

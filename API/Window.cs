using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace API
{
    /// <summary>
    /// Handles the size and other settings of the app's window.
    /// </summary>
    public static class Window
    {
        /// <summary>
        /// Represents a const of the default width of the window.
        /// </summary>
        public const int MAIN_WIDTH = 120;
        /// <summary>
        /// Represents a const of the default height of the window.
        /// </summary>
        public const int MAIN_HEIGHT = 30;

        /// <summary>
        /// Represents the width of the window.
        /// </summary>
        public static int width { get; private set; }
        /// <summary>
        /// Represents the height of the window.
        /// </summary>
        public static int height { get; private set; }
        /// <summary>
        /// Represents the title of the window.
        /// </summary>
        public static string title
        {
            get { return Console.Title; }
            set
            {
                if (!string.IsNullOrEmpty(value)) { Console.Title = value; }
                else { ExceptionsManager.TheConsoleTitleCantBeEmpty(value); }
            }
        }

        /// <summary>
        /// Initializes the app's window.
        /// </summary>
        /// <param name="width">The width of the window.</param>
        /// <param name="height">The height of the window.</param>
        /// <param name="windowTitle">The title of the window.</param>
        /// <param name="setBufferSize">If true, changes the console buffer size too.</param>
        internal static void Init(int width, int height, string windowTitle, bool setBufferSize = true)
        {
            try
            {
                Console.SetWindowSize(width, height);
                if (setBufferSize) { Console.SetBufferSize(width, height); }
                Window.width = width;
                Window.height = height;
            }
            catch (Exception e) { ExceptionsManager.ErrorResizingWindow(e); }
            Window.title = windowTitle;
        }

        /// <summary>
        /// Sets the window size.
        /// </summary>
        /// <param name="width">The width of the window.</param>
        /// <param name="height">The height of the window.</param>
        /// <param name="setBufferSize">If true, changes the console buffer size too.</param>
        public static void SetWindowSize(int width, int height, bool setBufferSize = true)
        {
            try
            {
                Console.SetWindowSize(width, height);
                if (setBufferSize) { Console.SetBufferSize(width, height); }
                Window.width = width;
                Window.height = height;
            }
            catch (Exception e) { ExceptionsManager.ErrorResizingWindow(e); }
        }
    }
}

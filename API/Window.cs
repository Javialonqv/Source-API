using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace API
{
    public static class Window
    {
        public const int MAIN_WIDTH = 120;
        public const int MAIN_HEIGHT = 30;

        public static int width { get; private set; }
        public static int height { get; private set; }
        public static string title
        {
            get { return Console.Title; }
            set
            {
                if (!string.IsNullOrEmpty(value)) { Console.Title = value; }
            }
        }

        internal static void Init(int width, int height, string windowTitle, bool setBufferSize = true)
        {
            //int i = Console.WindowWidth;
            //int j = Console.WindowHeight;
            Console.SetWindowSize(width, height);
            if (setBufferSize) { Console.SetBufferSize(width, height); }
            Window.width = width;
            Window.height = height;
            Window.title = windowTitle;
        }

        public static void SetWindowSize(int width, int height, bool setBufferSize = true)
        {
            Console.SetWindowSize(width, height);
            if (setBufferSize) { Console.SetBufferSize(width, height); }
            Window.width = width;
            Window.height = height;
        }
    }
}

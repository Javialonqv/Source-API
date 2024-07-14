using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace API.GameEngine
{
    /// <summary>
    /// The component for GameObjects to render text on the screen.
    /// </summary>
    public class Text : Component
    {
        /// <summary>
        /// The text to render.
        /// </summary>
        public string text { get; set; }
        /// <summary>
        /// The background color of the text.
        /// </summary>
        public ConsoleColor bgColor { get; set; }
        /// <summary>
        /// The color of the text.
        /// </summary>
        public ConsoleColor color { get; set; }

        internal override void Render()
        {
            // Should we render this?
            if (position.x < 0 || position.x >= Console.BufferWidth || position.y < 0 || position.y >= Console.BufferHeight) { return; }

            Console.SetCursorPosition((int)position.x, (int)position.y);
            Console.BackgroundColor = bgColor;
            Console.ForegroundColor = color;
            float startYPos = position.y;
            foreach (char c in text) // Iterate for each character in the text string.
            {
                if (Console.CursorLeft + 1 >= Console.BufferWidth) { break; } // Idk, it works on my machine. ¯\_(ツ)_/¯
                Console.Write(c);
            }
        }
    }
}

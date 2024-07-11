using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace API.GameEngine
{
    public class Text : Component
    {
        public string text { get; set; }

        internal override void Render()
        {
            // Should we render this?
            if (position.x < 0 || position.x >= Console.BufferWidth || position.y < 0 || position.y >= Console.BufferHeight) { return; }

            Console.SetCursorPosition((int)position.x, (int)position.y);
            float startYPos = position.y;
            foreach (char c in text) // Iterate for each character in the text string.
            {
                if (Console.CursorLeft + 1 >= Console.BufferWidth) { break; } // Idk, it works on my machine. ¯\_(ツ)_/¯
                Console.Write(c);
            }
        }
    }
}

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
            Console.SetCursorPosition((int)position.x, (int)position.y);
            Console.Write(text);
        }
    }
}

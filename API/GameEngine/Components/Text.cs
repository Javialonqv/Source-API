using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace API.GameEngine.Components
{
    public class Text : Component
    {
        public string text;
        public Text() { }
        public Text(string text)
        {
            this.text = text;
        }
    }
}

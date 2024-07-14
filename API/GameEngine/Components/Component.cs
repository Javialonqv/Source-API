using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace API.GameEngine
{
    /// <summary>
    /// The main class for adding components to a GameObject.
    /// </summary>
    public class Component
    {
        /// <summary>
        /// The GameObject where the class is attached.
        /// </summary>
        public GameObject gameObject { get; internal set; }
        /// <summary>
        /// The position of the GameObject where the class is attached.
        /// </summary>
        public Vector2 position { get { return gameObject.position; } set { gameObject.position = value; } }

        /// <summary>
        /// Creates a new instance of a Component class with a new GameObject.
        /// </summary>
        public Component()
        {
            gameObject = new GameObject();
        }

        internal virtual void Render() { }
    }
}

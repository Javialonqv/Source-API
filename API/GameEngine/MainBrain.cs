using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace API.GameEngine
{
    /// <summary>
    /// Main class for the managment of the Source Game Engine classes.
    /// </summary>
    public class MainBrain : Component
    {
        /// <summary>
        /// The GameObject where the class is attached.
        /// </summary>
        public GameObject gameObject { get; internal set; }
        /// <summary>
        /// The position of the GameObject where the class is attached.
        /// </summary>
        public Vector2 positon { get { return gameObject.position; } set { gameObject.position = value; } }

        /// <summary>
        /// Creates a new instance of a MainBrain class with a new GameObject.
        /// </summary>
        public MainBrain()
        {
            gameObject = new GameObject();
        }

        /// <summary>
        /// This method is called at the start of the runtime.
        /// </summary>
        public virtual void Start()
        {

        }
        /// <summary>
        /// This method is called every frame.
        /// </summary>
        public virtual void Update()
        {

        }

        /// <summary>
        /// Adds a new component into the GameObject.
        /// </summary>
        /// <typeparam name="T">The component type.</typeparam>
        /// <returns>An instance of the created component.</returns>
        public T AddComponent<T>() where T : Component, new()
        {
            return gameObject.AddComponent<T>();
        }
        /// <summary>
        /// Gets a component from the GameObject.
        /// </summary>
        /// <typeparam name="T">The component type to get.</typeparam>
        /// <returns>The instance of the component if it exists.</returns>
        public T GetComponent<T>() where T : Component
        {
            return gameObject.GetComponent<T>();
        }
        /// <summary>
        /// Tries to get a component from the GameObject.
        /// </summary>
        /// <typeparam name="T">The component type to get.</typeparam>
        /// <param name="component">A variable to return the instance of the component.</param>
        /// <returns>The specified component really exists?</returns>
        public bool TryGetComponent<T>(out T component) where T : Component
        {
            return gameObject.TryGetComponent<T>(out component);
        }

        /// <summary>
        /// Logs an object if the logger it's active.
        /// </summary>
        /// <param name="message">The object to print on the logger's console.</param>
        public static void print(object message)
        {
            Debug.Log(message);
        }
    }
}

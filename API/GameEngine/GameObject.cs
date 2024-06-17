using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;

namespace API.GameEngine
{
    /// <summary>
    /// The main class to instantiate objects into the game using the Source Game Engine.
    /// </summary>
    public class GameObject
    {
        /// <summary>
        /// Specifies the name of the object.
        /// </summary>
        public string name { get; set; }
        /// <summary>
        /// Specifies the position of the object.
        /// </summary>
        public Vector2 position { get; set; }
        GameObject Parent;
        public GameObject parent
        {
            get { return Parent; }
            set
            {
                Parent = value;
                Parent.childs.Add(this);
            }
        }
        internal List<GameObject> childs = new List<GameObject>();
        /// <summary>
        /// Specifies the list of components this object has.
        /// </summary>
        internal List<Component> components = new List<Component>();

        /// <summary>
        /// Creates a new GameObject with a default name.
        /// </summary>
        public GameObject()
        {
            // In case the Game class hasn't been initialized yet, throw an error.
            if (Game.gameInstance == null) { throw new InvalidOperationException("The Game class hasn't been initialized."); }

            // Just to change the GameObject's game. It can't be two or more with the same name.
            if (Find("New GameObject") == null) { name = "New GameObject"; }
            else
            {
                int i = 1;
                while (true)
                {
                    if (Find($"New GameObject ({i})") != null) { i++; }
                    else { name = $"New GameObject ({i})"; break; }
                }
            }
            Game.gameInstance.gameObjects.Add(this);
        }
        /// <summary>
        /// Creates a new GameObject with the specified name.
        /// </summary>
        /// <param name="name">The name of the object.</param>
        public GameObject(string name)
        {
            // In case the Game class hasn't been initialized yet, throw an error.
            if (Game.gameInstance == null) { throw new InvalidOperationException("The Game class hasn't been initialized."); }

            // Just to change the GameObject's game. It can't be two or more with the same name.
            if (Find("New GameObject") == null) { this.name = name; }
            else
            {
                int i = 1;
                while (true)
                {
                    if (Find($"{name} ({i})") != null) { i++; }
                    else { this.name = $"{name} ({i})"; break; }
                }
            }
            Game.gameInstance.gameObjects.Add(this);
        }

        /// <summary>
        /// Adds a new component into the GameObject.
        /// </summary>
        /// <typeparam name="T">The component type.</typeparam>
        /// <returns>An instance of the created component.</returns>
        public T AddComponent<T>() where T : Component, new()
        {
            Component newComponent = new T();
            if (typeof(MainBrain).IsAssignableFrom(typeof(T)))
            {
                ((MainBrain)newComponent).gameObject = this;
            }
            components.Add(newComponent);
            return (T)newComponent;
        }
        /// <summary>
        /// Gets a component from the GameObject.
        /// </summary>
        /// <typeparam name="T">The component type to get.</typeparam>
        /// <returns>The instance of the component if it exists.</returns>
        public T GetComponent<T>() where T : Component
        {
            return (T)components.Find(c => c is T);
        }
        /// <summary>
        /// Tries to get a component from the GameObject.
        /// </summary>
        /// <typeparam name="T">The component type to get.</typeparam>
        /// <param name="component">A variable to return the instance of the component.</param>
        /// <returns>The specified component really exists?</returns>
        public bool TryGetComponent<T>(out T component) where T : Component
        {
            component = (T)components.FirstOrDefault(c => c is T);
            return component != null;
        }

        public GameObject GetChild(int index)
        {
            if (index > childs.Count - 1) { throw new IndexOutOfRangeException("The index was outside of the number of childs of the current object."); }
            return childs[index];
        }

        /// <summary>
        /// Tries to find a GameObject with the specified name.
        /// </summary>
        /// <param name="name">The name of the object.</param>
        /// <returns>The GameObject of the specified name if it exists.</returns>
        public static GameObject Find(string name)
        {
            return Game.gameInstance.gameObjects.Find(obj => obj.name == name);
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;
using System.Runtime.CompilerServices;

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
        /// <summary>
        /// Specifies the parent object of this one.
        /// </summary>
        public GameObject parent
        {
            get { return Parent; }
            set
            {
                Parent = value;
                Parent.childs.Add(this);
            }
        }
        /// <summary>
        /// Specifies the list of the childs of this object.
        /// </summary>
        internal List<GameObject> childs = new List<GameObject>();
        /// <summary>
        /// Specifies the list of components this object has.
        /// </summary>
        internal List<Component> components = new List<Component>();
        /// <summary>
        /// Specifies if the current object is active in the hierarchy of objects.
        /// </summary>
        public bool activeInHierarchy
        {
            get
            {
                if (Parent == null) { return activeSelf; }
                else { return Parent.activeInHierarchy; }
            }
        }
        /// <summary>
        /// The local active state of this GameObject.
        /// </summary>
        public bool activeSelf { get; private set; } = true;
        /// <summary>
        /// The tag that identifies this GameObject.
        /// </summary>
        public string tag { get; set; }

        /// <summary>
        /// Creates a new GameObject with a default name.
        /// </summary>
        public GameObject()
        {
            // In case the Game class hasn't been initialized yet, throw an error.
            if (Game.gameInstance == null) { ExceptionsManager.GameClassNotInitializedYet(); return; }

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
            position = new Vector2(0, 0);

            Game.gameInstance.gameObjects.Add(this);
        }
        /// <summary>
        /// Creates a new GameObject with the specified name.
        /// </summary>
        /// <param name="name">The name of the object.</param>
        public GameObject(string name)
        {
            // In case the Game class hasn't been initialized yet, throw an error.
            if (Game.gameInstance == null) { ExceptionsManager.GameClassNotInitializedYet(); return; }

            // Just to change the GameObject's name. It can't be two or more with the same name.
            if (Find(name) == null) { this.name = name; }
            else
            {
                int i = 1;
                while (true)
                {
                    if (Find($"{name} ({i})") != null) { i++; }
                    else { this.name = $"{name} ({i})"; break; }
                }
            }
            position = new Vector2(0, 0);

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
            newComponent.gameObject = this;
            components.Add(newComponent);
            MainBrain.InitRequiredComponents(typeof(T), newComponent);
            return (T)newComponent;
        }
        /// <summary>
        /// Adds a new component into the GameObject.
        /// </summary>
        /// <param name="component">The component type.</param>
        /// <returns>An instance of the created component.</returns>
        public Component AddComponent(Type component)
        {
            if (!component.IsSubclassOf(typeof(Component))) { ExceptionsManager.TypeDoesntInheritFromComponent(component.FullName); return null; }

            Component newComponent = (Component)MainBrain.CreateInstance(component);
            newComponent.gameObject = this;
            components.Add(newComponent);
            MainBrain.InitRequiredComponents(component, newComponent);
            return newComponent;
        }

        /// <summary>
        /// Gets a component from the GameObject.
        /// </summary>
        /// <typeparam name="T">The component type to get.</typeparam>
        /// <returns>The instance of the component if it exists.</returns>
        public T GetComponent<T>() where T : Component
        {
            if (components.FirstOrDefault(c => c is T) == null) { ExceptionsManager.CantFindSpecifiedComponent(name); return null; }
            return (T)components.Find(c => c is T);
        }
        /// <summary>
        /// Gets a component from the GameObject.
        /// </summary>
        /// <param name="componentType">The component type to get.</param>
        /// <returns>The instance of the component if it exists.</returns>
        public Component GetComponent(Type componentType)
        {
            if (components.FirstOrDefault(c => c.GetType() == componentType) == null) { return null;  }
            return components.Find(c => c.GetType() == componentType);
        }
        /// <summary>
        /// Gets all the components as the same type from the GameObject.
        /// </summary>
        /// <typeparam name="T">The component type to get.</typeparam>
        /// <returns>An array of components from the specified type.</returns>
        public T[] GetComponents<T>() where T : Component
        {
            return components.Where(c => c is T).Select(c => c as T).ToArray();
        }
        /// <summary>
        /// Gets all the components as the same type from the GameObject.
        /// </summary>
        /// <param name="componentType">The component type to get.</param>
        /// <returns>An array of components from the specified type.</returns>
        public Component[] GetComponents(Type componentType)
        {
            return components.Where(c => c.GetType() == componentType).ToArray();
        }

        /// <summary>
        /// Tries to get a component from the GameObject.
        /// </summary>
        /// <typeparam name="T">The component type to get.</typeparam>
        /// <param name="component">A variable to return the instance of the component.</param>
        /// <returns>The specified component really exists?</returns>
        public bool TryGetComponent<T>(out T component) where T : Component
        {
            try
            {
                component = (T)components.FirstOrDefault(c => c is T);
                return component != null;
            }
            catch
            {
                component = null;
                return false;
            }
        }
        /// <summary>
        /// Tries to get a component from the GameObject.
        /// </summary>
        /// <param name="componentType">The component type to get.</param>
        /// <param name="component">A variable to return the instance of the component.</param>
        /// <returns>The specified component really exists?</returns>
        public bool TryGetComponent(Type componentType, out Component component)
        {
            try
            {
                component = components.FirstOrDefault(c => c.GetType() == componentType);
                return component != null;
            }
            catch
            {
                component = null;
                return false;
            }
        }

        /// <summary>
        /// Returns a child by index.
        /// </summary>
        /// <param name="index">The index of the child to return.</param>
        /// <returns>Returns a child by index.</returns>
        /// <exception cref="IndexOutOfRangeException"></exception>
        public GameObject GetChild(int index)
        {
            if (index > childs.Count - 1) { ExceptionsManager.IndexOutsideOfBoundsOfTheNumberOfChilds(childs.Count, index); return null; }
            return childs[index];
        }
        /// <summary>
        /// Activates/Deactivates the GameObject, depending on the given true or false value.
        /// </summary>
        /// <param name="state">The new state of the GameObject.</param>
        public void SetActive(bool state)
        {
            activeSelf = state;
        }

        /// <summary>
        /// Tries to find a GameObject with the specified name.
        /// </summary>
        /// <param name="name">The name of the object.</param>
        /// <returns>The GameObject of the specified name if it exists.</returns>
        public static GameObject Find(string name)
        {
            return Game.gameInstance.gameObjects.FirstOrDefault(obj => obj.name == name && obj.activeInHierarchy);
        }
        /// <summary>
        /// Tries to find a GameObject with the specified name.
        /// </summary>
        /// <param name="name">The name of the object.</param>
        /// <param name="includeInactive">Specifies if it should look for inactive objects in hierarchy too.</param>
        /// <returns>The GameObject of the specified name if it exists.</returns>
        public static GameObject Find(string name, bool includeInactive)
        {
            if (includeInactive) { return Game.gameInstance.gameObjects.FirstOrDefault(obj => obj.name == name); } // Just look for ACTIVE objects.
            else { return Game.gameInstance.gameObjects.FirstOrDefault(obj => obj.name == name && obj.activeInHierarchy); } // Look for all of them.
        }
        /// <summary>
        /// Tries to find a GameObject with the specified tag.
        /// </summary>
        /// <param name="tag">The tag of the GameObject.</param>
        /// <returns>Returns a GameObject tagget with the specified tag.</returns>
        public static GameObject FindWithTag(string tag)
        {
            return Game.gameInstance.gameObjects.FirstOrDefault(obj => obj.tag == tag);
        }
        /// <summary>
        /// Tries to find all the GameObjects with the specified tag.
        /// </summary>
        /// <param name="tag">The tag of the GameObjects.</param>
        /// <returns>Returns an array of the GameObjects with the specified tag.</returns>
        public static GameObject[] FindGameObjectsWithTag(string tag)
        {
            GameObject[] objects = Game.gameInstance.gameObjects.Where(obj => obj.tag == tag).ToArray();
            return objects.Length > 0 ? objects : null;
        }

        /// <summary>
        /// Is this GameObject tagget with tag?
        /// </summary>
        /// <param name="tag">The tag to compare.</param>
        /// <returns>Returns true if the GameObject is tagged with the specified one.</returns>
        public bool CompareTag(string tag)
        {
            return this.tag == tag;
        }
    }
}

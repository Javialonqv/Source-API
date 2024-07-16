using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;
using System.Threading;
using NAudio.Wave;
using System.Diagnostics;

namespace API.GameEngine
{
    /// <summary>
    /// Main class for the managment of the Source Game Engine classes.
    /// </summary>
    public class MainBrain : Component
    {
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

        /// <summary>
        /// Invokes a method in time seconds.
        /// </summary>
        /// <param name="method">The method to invoke.</param>
        /// <param name="delay">The amount of time in seconds to wait until the method is executed.</param>
        public static void Invoke(Action method, float delay)
        {
            Thread thread = new Thread(new ThreadStart(() =>
            {
                Thread.Sleep((int)(delay * 1000));
                method();
            }));
            thread.Start();
        }

        /// <summary>
        /// Tries to find an object of the specified type.
        /// </summary>
        /// <typeparam name="T">The type to find.</typeparam>
        /// <returns>Returns the first loaded object of the specified type.</returns>
        public static T FindObjectOfType<T>() where T : Component
        {
            foreach (GameObject obj in Game.gameInstance.gameObjects)
            {
                if (obj.TryGetComponent<T>(out T component))
                {
                    return component;
                }
            }
            return null;
        }
        /// <summary>
        /// Tries to find an object of the specified type.
        /// </summary>
        /// <param name="type">The type to find.</param>
        /// <returns>Returns the first loaded object of the specified type.</returns>
        public static object FindObjectOfType(Type type)
        {
            foreach (GameObject obj in Game.gameInstance.gameObjects)
            {
                Component component = obj.components.FirstOrDefault(c => c.GetType() == type);
                if (component != null)
                {
                    return component;
                }
            }
            return null;
        }
        /// <summary>
        /// Tries to find all the objects of the specified type.
        /// </summary>
        /// <typeparam name="T">The type to find.</typeparam>
        /// <returns>Returns all the loaded objects of the specified type.</returns>
        public static T[] FindObjectsOfType<T>() where T : Component
        {
            List<T> listToReturn = new List<T>();

            foreach (GameObject obj in Game.gameInstance.gameObjects)
            {
                if (obj.TryGetComponent<T>(out T component))
                {
                    listToReturn.Add(component);
                }
            }
            return listToReturn.Count > 0 ? listToReturn.ToArray() : null;
        }
        /// <summary>
        /// Tries to find all the objects of the specified type.
        /// </summary>
        /// <param name="type">The type to find.</param>
        /// <returns>Returns all the loaded objects of the specified type.</returns>
        public static object[] FindObjectsOfType(Type type)
        {
            List<object> listToReturn = new List<object>();
            foreach (GameObject obj in Game.gameInstance.gameObjects)
            {
                Component component = obj.components.FirstOrDefault(c => c.GetType() == type);
                if (component != null)
                {
                    listToReturn.Add(component);
                }
            }
            return listToReturn.Count > 0 ? listToReturn.ToArray() : null;
        }

        internal static void CallMethod(string methodName, object instance)
        {
            Type type = instance.GetType();
            MethodInfo methodInfo = type.GetMethod(methodName, BindingFlags.Instance | BindingFlags.NonPublic);
            if (methodInfo != null)
            {
                methodInfo.Invoke(instance, null);
            }
        }

        internal static bool HasAttribute(Type type, Type attribute)
        {
            return Attribute.IsDefined(type, attribute);
        }

        internal static void InitRequiredComponents(Type componentType, Component componentInstance)
        {
            object[] attributes = componentType.GetCustomAttributes(typeof(RequireComponent), false); // Get all the RequireComponent attributes from that class.
            if (attributes != null) // If there are attributes
            {
                if (attributes.Length > 0) // If there are attributes
                {
                    foreach (var attribute in attributes) // Foreach every attribute that was found:
                    {
                        RequireComponent currentAttribute = attribute as RequireComponent; // Get the attribute.
                        if (currentAttribute.requiredComponent == null) { return; }
                        // If there's an instance of the required component already, do nothing and return:
                        if (componentInstance.gameObject.GetComponent(currentAttribute.requiredComponent) != null) { return; }
                        // Otherwise, create a new instance:
                        object newInstance = Activator.CreateInstance(currentAttribute.requiredComponent);
                        ((Component)newInstance).gameObject = componentInstance.gameObject;
                        componentInstance.gameObject.components.Add((Component)newInstance);
                    }
                }
            }
        }
    }
}

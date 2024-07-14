using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;
using System.Threading;
using NAudio.Wave;

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
                    foreach (var attribute in attributes)
                    {
                        RequireComponent currentAttribute = attribute as RequireComponent;
                        // Check for every component inside of the gameobject attached to that component class.
                        foreach (Component component in componentInstance.gameObject.components)
                        {
                            if (currentAttribute.componentType == component.GetType()) { return; }
                        }
                        // In case we need to create a new instance:
                        object newInstance = Activator.CreateInstance(currentAttribute.componentType);
                        ((Component)newInstance).gameObject = componentInstance.gameObject;
                        componentInstance.gameObject.components.Add((Component)newInstance);
                    }
                }
            }
        }
    }
}

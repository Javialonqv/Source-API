using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using API.GameEngine.Components;

namespace API.GameEngine
{
    /// <summary>
    /// Represents an Game Engine object.
    /// </summary>
    public class GameObject
    {
        /// <summary>
        /// Specifies the name of the GameObject.
        /// </summary>
        public string name;
        public Vector2 position;
        List<GameObject> childs = new List<GameObject>();
        public int childCount = 0;
        List<Components.Component> components = new List<Components.Component>();

        public GameObject() { MainBrain.gameObjects.Add(this); }
        public GameObject(string name)
        {
            MainBrain.gameObjects.Add(this);
            this.name = name;
        }

        public GameObject GetChild(int index)
        {
            return childs[index];
        }

        public T GetComponent<T>() where T : Components.Component
        {
            return (T)components.Find(c => c.GetType() == typeof(T));
        }
        public bool TryGetComponent<T>(out T component) where T : Components.Component
        {
            component = (T)components.Find(c => c.GetType() == typeof(T));
            return component != null;
        }
        public T AddComponent<T>() where T : Components.Component, new()
        {
            T newComponent = new T();
            components.Add(newComponent);
            if (newComponent is MainBrain) { MainBrain.classes.Add(newComponent as MainBrain); }
            return newComponent;
        }
    }
}

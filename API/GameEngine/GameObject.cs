using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;

namespace API.GameEngine
{
    public class GameObject
    {
        public string name { get; set; }
        public Vector2 position { get; set; }
        List<Component> components = new List<Component>();

        public GameObject()
        {
            name = "New GameObject";
            Game.gameInstance.gameObjects.Add(this);
        }
        public GameObject(string name)
        {
            this.name = name;
            Game.gameInstance.gameObjects.Add(this);
        }

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
        public T GetComponent<T>() where T : Component
        {
            return (T)components.Find(c => c is T);
        }
        public bool TryGetComponent<T>(out T component) where T : Component
        {
            component = (T)components.FirstOrDefault(c => c is T);
            return component != null;
        }

        public static GameObject Find(string name)
        {
            return Game.gameInstance.gameObjects.Find(obj => obj.name == name);
        }
    }
}

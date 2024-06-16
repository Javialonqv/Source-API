using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace API.GameEngine
{
    public class MainBrain : Component
    {
        public GameObject gameObject { get; internal set; }
        public Vector2 positon { get { return gameObject.position; } set { gameObject.position = value; } }
        internal static List<MainBrain> classes = new List<MainBrain>();

        internal MainBrain(GameObject obj)
        {
            gameObject = obj;
            classes.Add(this);
        }

        public MainBrain()
        {
            gameObject = new GameObject();
            classes.Add(this);
        }

        public virtual void Start()
        {

        }
        public virtual void Update()
        {

        }

        public T AddComponent<T>() where T : Component, new()
        {
            return gameObject.AddComponent<T>();
        }
        public T GetComponent<T>() where T : Component
        {
            return gameObject.GetComponent<T>();
        }
    }
}

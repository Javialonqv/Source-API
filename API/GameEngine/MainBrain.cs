using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using API.GameEngine.Components;

namespace API.GameEngine
{
    public class MainBrain : Component
    {
        internal static List<GameObject> gameObjects = new List<GameObject>();
        internal static List<MainBrain> classes = new List<MainBrain>();

        public MainBrain()
        {
            //classes.Add(this);
        }

        public void print(object obj)
        {
            Debug.Log(obj);
        }

        public virtual void Start()
        {

        }

        public virtual void Update()
        {

        }
    }
}

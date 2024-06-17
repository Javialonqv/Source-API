using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace API.GameEngine
{
    /// <summary>
    /// Main class for the creating of a game using the Source Game Engine.
    /// </summary>
    public class Game
    {
        /// <summary>
        /// Specifies the ONLY instance of this class running in the runtime.
        /// </summary>
        internal static Game gameInstance;
        /// <summary>
        /// Specifies if the game is running.
        /// </summary>
        internal bool isRunning = false;
        /// <summary>
        /// Specifies all the game objects instantiated in the runtime.
        /// </summary>
        internal List<GameObject> gameObjects = new List<GameObject>();
        /// <summary>
        /// Specifies the frame rate of the game/app.
        /// </summary>
        internal int targetFrameRate = 30;

        /// <summary>
        /// Creates a new Game class.
        /// </summary>
        public Game()
        {
            if (gameInstance != null) { throw new InvalidOperationException("A Game class instance has already been initialized."); }
            gameInstance = this;
        }

        /// <summary>
        /// Inits the game engine and call the Start() method of all the MainBrain classes instantiated before this method executes.
        /// </summary>
        public void Run()
        {
            if (gameInstance != this) { return; }
            int frameTime = 1000 / targetFrameRate;
            isRunning = true;
            Start();
            while (isRunning)
            {
                DateTime startTime = DateTime.Now;

                Update();

                // Calculate delta time and sleep the thread.
                DateTime endTime = DateTime.Now;
                int elapsed = (int)(endTime - startTime).TotalMilliseconds;
                Time.deltaTime = frameTime - elapsed;
                Time.time += Time.deltaTime;
                if (elapsed < frameTime)
                {
                    Thread.Sleep(frameTime - elapsed);
                }
            }
        }

        /// <summary>
        /// Calls all the Start() method on all the MainBrain instantiaded classes.
        /// </summary>
        void Start()
        {
            foreach (GameObject obj in gameObjects) // Iterate for each gameobject.
            {
                foreach (Component component in obj.components) // Iterate for each componenet of the currrent gameobject.
                {
                    if (component is MainBrain) { MainBrain.CallMethod("Start", (MainBrain)component); } // If the component is a MainBrain class, call the method.
                }
            }
        }
        /// <summary>
        /// Calls all the Update() method on all the MainBrain instantiaded classes.
        /// </summary>
        void Update()
        {
            foreach (GameObject obj in gameObjects) // Iterate for each gameobject.
            {
                foreach (Component component in obj.components) // Iterate for each componenet of the currrent gameobject.
                {
                    if (component is MainBrain) { MainBrain.CallMethod("Update", (MainBrain)component); } // If the component is a MainBrain class, call the method.
                }
            }
        }
    }
}

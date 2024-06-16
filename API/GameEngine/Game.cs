using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace API.GameEngine
{
    public class Game
    {
        internal static Game gameInstance;
        internal bool isRunning = false;
        internal List<GameObject> gameObjects = new List<GameObject>();
        internal int targetFrameRate = 30;

        public Game()
        {
            if (gameInstance != null) { return; }
            gameInstance = this;
        }

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

        void Start()
        {
            foreach (MainBrain brain in MainBrain.classes)
            {
                brain.Start();
            }
        }
        void Update()
        {
            foreach (MainBrain brain in MainBrain.classes)
            {
                brain.Update();
            }
        }
    }
}

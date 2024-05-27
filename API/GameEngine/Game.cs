using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using API.GameEngine.Components;

namespace API.GameEngine
{
    /// <summary>
    /// Manage all the functions related with the Game Engine.
    /// </summary>
    public class Game
    {
        /// <summary>
        /// Static instance of the Game class.
        /// </summary>
        static Game Instance = null;

        /// <summary>
        /// Specifies if the game is running.
        /// </summary>
        bool isRunning = false;
        /// <summary>
        /// The target frame rate.
        /// </summary>
        int targetFrameRate = 30;

        /// <summary>
        /// Creates an instance of the Game class.
        /// </summary>
        public Game()
        {
            // If another instance of the class is running, do nothing.
            if (Instance == null) { Instance = this; }
            else { return; }
        }

        /// <summary>
        /// Runs the game. Make sure to instance all the MainBrain classes before calling this method.
        /// </summary>
        public void Run()
        {
            Console.CursorVisible = false; // Hide the cursor.
            isRunning = true; // Specify the running state.
            int frameTime = 1000 / targetFrameRate;
            Start(); // Call the Start() method in all the MainBrain classes.
            while (isRunning)
            {
                DateTime startTime = DateTime.Now;

                Update();

                Render();

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
        /// This method is called once at the start of the runtime.
        /// </summary>
        static void Start()
        {
            foreach (MainBrain Class in MainBrain.classes)
            {
                Class.Start();
            }
        }

        /// <summary>
        /// This method is called once per frame.
        /// </summary>
        static void Update()
        {
            Input.Update();
            foreach (MainBrain Class in MainBrain.classes)
            {
                Class.Update();
            }
        }

        /// <summary>
        /// Render all the GameObjects with a Text component on it.
        /// </summary>
        static void Render()
        {
            foreach (GameObject obj in MainBrain.gameObjects)
            {
                if (obj != null)
                {
                    if (obj.TryGetComponent<Text>(out Text textObj))
                    {
                        Console.SetCursorPosition((int)obj.position.X, (int)obj.position.Y);
                        Console.Write(textObj.text);
                    }
                }
            }
        }
    }
}

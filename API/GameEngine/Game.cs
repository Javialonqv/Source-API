using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using API.GameEngine.Components;

namespace API.GameEngine
{
    public class Game
    {
        static Game Instance = null;

        bool isRunning = false;
        int targetFrameRate = 30;

        public Game()
        {
            if (Instance == null) { Instance = this; }
            else { return; }
        }

        public void Run()
        {
            Console.CursorVisible = false;
            isRunning = true;
            int frameTime = 1000 / targetFrameRate;
            Start();
            while (isRunning)
            {
                DateTime startTime = DateTime.Now;

                Update();

                Render();

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

        static void Start()
        {
            foreach (MainBrain Class in MainBrain.classes)
            {
                Class.Start();
            }
        }

        static void Update()
        {
            Input.Update();
            foreach (MainBrain Class in MainBrain.classes)
            {
                Class.Update();
            }
        }

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

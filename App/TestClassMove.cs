using API;
using App;
using System;
using System.Collections.Generic;
using System.Linq;
using API.GameEngine;

namespace App
{
    public class TestClassMove : MainBrain
    {
        // Start is called at the start of the runtime.
        void Start()
        {
            
        }

        // Update is called every frame.
        void Update()
        {
            if (Input.GetKey(ConsoleKey.Spacebar)) position += new Vector2(0, 1f);
        }
    }
}

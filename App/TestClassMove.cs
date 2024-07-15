using API;
using App;
using System;
using System.Collections.Generic;
using System.Linq;
using API.GameEngine;

namespace App
{
    [ExecuteOnGameObjectDisabled]
    [RequireComponent(typeof(Text))]
    public class TestClassMove : MainBrain
    {
#pragma warning disable
        // Start is called at the start of the runtime.
        void Start()
        {
            
        }

        // Update is called every frame.
        void Update()
        {
            if (Input.GetButtonDown("Jump"))
            {
                gameObject.SetActive(!gameObject.activeSelf);
                print("Changed to: " + gameObject.activeSelf);
            }
        }
    }
}

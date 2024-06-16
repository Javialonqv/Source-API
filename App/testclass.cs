using API.GameEngine;
using API;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App
{
    internal class testclass : MainBrain
    {
        public override void Start()
        {
            Debug.Log(gameObject.name);
        }
    }
}

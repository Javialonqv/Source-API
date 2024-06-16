using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace API.GameEngine
{
    public static class Time
    {
        static int realTime;
        public static int time
        {
            get { return realTime; }
            internal set { realTime = value; }
        }
        static int DeltaTime;
        public static int deltaTime
        {
            get { return DeltaTime; }
            internal set { DeltaTime = value; }
        }
    }
}

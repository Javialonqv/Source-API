using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace API.GameEngine
{
    /// <summary>
    /// Class for the managment of the Time on the Source Game Engine.
    /// </summary>
    public static class Time
    {
        static float realTime;
        /// <summary>
        /// The elapsed time since the runtime start.
        /// </summary>
        public static float time
        {
            get { return realTime; }
            internal set { realTime = value; }
        }
        static float DeltaTime;
        /// <summary>
        /// The interval in seconds from the last frame to the current one.
        /// </summary>
        public static float deltaTime
        {
            get { return DeltaTime; }
            internal set { DeltaTime = value / 1000; }
        }
    }
}

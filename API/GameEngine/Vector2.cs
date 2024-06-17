using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace API.GameEngine
{
    public class Vector2
    {
        float X, Y;
        /// <summary>
        /// X component of the vector.
        /// </summary>
        public float x
        {
            get { return X; }
            set
            {
                if (value < 0) { X = 0; } else { X = value; }
            }
        }
        /// <summary>
        /// Y component of the vector.
        /// </summary>
        public float y
        {
            get { return Y; }
            set
            {
                if (value < 0) { Y = 0; } else { Y = value; }
            }
        }

        /// <summary>
        /// Creates a new Vector2 with the specified values.
        /// </summary>
        /// <param name="x">X component of the vector.</param>
        /// <param name="y">Y component of the vector.</param>
        public Vector2(float x, float y)
        {
            this.x = x;
            this.y = y;
        }

        public static Vector2 operator +(Vector2 v1, Vector2 v2) { return new Vector2(v1.x + v2.x, v1.y + v2.y); }
        public static Vector2 operator -(Vector2 v1, Vector2 v2) { return new Vector2(v1.x - v2.x, v1.y - v2.y); }
        public static Vector2 operator /(Vector2 v1, Vector2 v2) { return new Vector2(v1.x / v2.x, v1.y / v2.y); }
        public static Vector2 operator *(Vector2 v1, Vector2 v2) { return new Vector2(v1.x * v2.x, v1.y * v2.y); }
    }
}

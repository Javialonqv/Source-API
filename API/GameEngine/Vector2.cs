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
        public float x
        {
            get { return X; }
            set
            {
                if (value < 0) { X = 0; } else { X = value; }
            }
        }
        public float y
        {
            get { return Y; }
            set
            {
                if (value < 0) { Y = 0; } else { Y = value; }
            }
        }

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

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace API.GameEngine
{
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = true, Inherited = true)]
    public class RequireComponent : Attribute
    {
        public Type componentType;

        public RequireComponent(Type componentType)
        {
            this.componentType = componentType;
        }
    }
}

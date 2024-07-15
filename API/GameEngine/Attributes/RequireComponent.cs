using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace API.GameEngine
{
    /// <summary>
    /// Specifies which component is required and adds it.
    /// </summary>
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = true, Inherited = true)]
    public class RequireComponent : Attribute
    {
        public Type requiredComponent;

        public RequireComponent(Type componentType)
        {
            this.requiredComponent = componentType;
        }
    }
}

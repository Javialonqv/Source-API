using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;

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
            if (componentType != typeof(Component) && !componentType.IsSubclassOf(typeof(Component)))
            {
                ExceptionsManager.TypeDoesntInheritFromComponent(componentType.FullName);
                requiredComponent = null;
                return;
            }
            requiredComponent = componentType;
        }
    }
}

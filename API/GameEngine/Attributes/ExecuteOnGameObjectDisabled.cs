using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace API.GameEngine
{
    /// <summary>
    /// Specifies if the class methods should be executed even if the GameObject isn't active.
    /// </summary>
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = false, Inherited = false)]
    public class ExecuteOnGameObjectDisabled : Attribute
    {

    }
}

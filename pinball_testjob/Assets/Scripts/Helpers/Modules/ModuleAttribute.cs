using System;

namespace Helpers.Modules {
    [AttributeUsage(AttributeTargets.Class)]
    public class ModuleAttribute : Attribute {
        public Type[] Dependecies = new Type[0];
        public string PrefabPath = string.Empty;
    }
}

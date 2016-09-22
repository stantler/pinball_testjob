using System;

namespace GUI.Base
{
    [AttributeUsage(AttributeTargets.Class)]
    public class GUIComponentAttribute : Attribute
    {
        public readonly int Layer;
        public readonly string PrefabPath;

        public GUIComponentAttribute(int layer, string prefabPath)
        {
            Layer = layer;
            PrefabPath = prefabPath;
        }
    }
}
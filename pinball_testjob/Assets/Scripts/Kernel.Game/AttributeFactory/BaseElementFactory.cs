using System;
using System.Collections.Generic;
using System.Reflection;

namespace Kernel.Game.AttributeFactory
{
    public interface IFactoryAttribute<out TCode>
    {
        TCode Code { get; }
    }

    public class BaseElementFactory<TCode, TAttribute, TElementBase>
        where TAttribute : Attribute, IFactoryAttribute<TCode>
    {
        private static readonly AssemblyClassesCache<TCode, TAttribute> Cache = new AssemblyClassesCache<TCode, TAttribute>((t, a) => a.Code);
        private readonly Dictionary<TCode, Type> _types;

        public BaseElementFactory(Assembly assembly)
        {
            _types = Cache[assembly];
        }

        public Type GetFactoryElementType(TCode code)
        {
            Type type;
            return _types.TryGetValue(code, out type) ? type : null;
        }

        public TElementBase GetFactoryElement(TCode code, params object[] paremeters)
        {
            var type = GetFactoryElementType(code);

            if (type == null)
            {
                return default(TElementBase);
            }

            return (TElementBase)Activator.CreateInstance(type, paremeters);
        }

        public TElement GetFactoryElement<TElement>(TCode code, params object[] paremeters) where TElement : TElementBase
        {
            var type = GetFactoryElementType(code);

            if (type == null)
            {
                return default(TElement);
            }

            return (TElement)Activator.CreateInstance(type, paremeters);
        }
    }
}
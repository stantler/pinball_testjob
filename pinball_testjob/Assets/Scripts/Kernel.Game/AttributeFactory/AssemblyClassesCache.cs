using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Kernel.Game.AttributeFactory
{
    public sealed class AssemblyClassesCache<TKey, TAttribute>
        where TAttribute : Attribute
    {
        private readonly Dictionary<Assembly, Dictionary<TKey, Type>> _cache;
        private readonly Func<Type, TAttribute, TKey> _keySelector;
        private readonly object _syncRoot = new object();

        public AssemblyClassesCache(Func<Type, TAttribute, TKey> keySelector)
        {
            _cache = new Dictionary<Assembly, Dictionary<TKey, Type>>();
            _keySelector = keySelector;
        }

        public Dictionary<TKey, Type> this[Assembly assembly]
        {
            get
            {
                lock (_syncRoot)
                {
                    Dictionary<TKey, Type> result;
                    return _cache.TryGetValue(assembly, out result)
                        ? result
                        : (_cache[assembly] = AssemblyClassesCacheBase.Get(assembly)
                            .SelectMany(t =>
                                t.GetCustomAttributes(typeof(TAttribute), false)
                                    .Cast<TAttribute>()
                                    .Select(attribute => new
                                    {
                                        Type = t,
                                        Attribute = attribute
                                    })
                            )
                            .Where(t => t.Attribute != null)
                            .ToDictionary(
                                t => _keySelector(t.Type, t.Attribute),
                                t => t.Type
                            ));
                }
            }
        }

        internal static class AssemblyClassesCacheBase
        {
            private static readonly Dictionary<Assembly, List<Type>> Cache = new Dictionary<Assembly, List<Type>>();
            private static readonly object SyncRoot = new object();

            public static IEnumerable<Type> Get(Assembly assembly)
            {
                lock (SyncRoot)
                {
                    List<Type> result;
                    return Cache.TryGetValue(assembly, out result)
                        ? result
                        : (Cache[assembly] = assembly.GetTypes().ToList());
                }
            }
        }
    }
}
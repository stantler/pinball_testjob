using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Helpers.Extension;
using UnityEngine;

namespace Helpers.Modules
{
    public static class ModuleInitializator
    {
        public static Transform Parent;

        private static bool Is(Type targeType, Type baseType)
        {
            while (targeType != null)
            {
                if (targeType == baseType)
                {
                    return true;
                }

                targeType = targeType.BaseType;
            }
            return false;
        }

        private static IModule InitializeMono(Type targetType, ModuleAttribute attribute)
        {
            GameObject serviceInstance = null;

            if (attribute != null)
            {
                if (!string.IsNullOrEmpty(attribute.PrefabPath))
                {
                    serviceInstance = attribute.PrefabPath.LoadAndInstantiate();
                }
            }

            if (serviceInstance == null)
            {
                serviceInstance = new GameObject();
            }

            serviceInstance.name = "" + targetType;
            serviceInstance.transform.SetParent(Parent);
            serviceInstance.transform.position = Vector3.zero;

            var serviceComponent = serviceInstance.GetComponent(targetType) ??
                                   serviceInstance.AddComponent(targetType);

            return (IModule)serviceComponent;
        }

        private static IModule InitializeService(Type targetType, ModuleAttribute attribute)
        {
            if (Is(targetType.BaseType, typeof(MonoBehaviour)))
            {
                return InitializeMono(targetType, attribute);
            }

            return (IModule)Activator.CreateInstance(targetType);
        }

        public static void InitializeServices(Type targetType, UnityEngine.MonoBehaviour coroutineComponent)
        {
            coroutineComponent.StartCoroutine(InitializeCoroutine(targetType));
        }

        private static IEnumerator InitializeCoroutine(Type targetType)
        {
            var properties = targetType.GetProperties(BindingFlags.Static | BindingFlags.Public);

            var initializedTypes = new List<Type>();
            var typesToInitialize = new Dictionary<PropertyInfo, ModuleAttribute>();
            var typesInTarget = properties.Select(p => p.PropertyType);

            foreach (var propertyInfo in properties)
            {
                var propertyType = propertyInfo.PropertyType;
                if (typeof(IModule).IsAssignableFrom(propertyType) == false)
                {
                    continue;
                }

                var attr = propertyType.GetCustomAttributes(typeof(ModuleAttribute), true).FirstOrDefault() as ModuleAttribute;
                if (attr != null && attr.Dependecies.Any(t => !initializedTypes.Contains(t)))
                {
                    if (attr.Dependecies.Any(t => !typesInTarget.Contains(t)))
                    {
                        Debug.LogError(string.Format("[ServiceInitializator] There is no such ServiceType [{0}] in load stack",
                            propertyType));
                    }
                    else if (attr.Dependecies.Any(t => t == propertyType))
                    {
                        Debug.LogError(string.Format("[ServiceInitializator] ServiceType depends on his own class [{0}]",
                            propertyType));
                    }
                    else
                    {
                        typesToInitialize.Add(propertyInfo, attr);
                        continue;
                    }
                }

                var service = InitializeService(propertyType, attr);

                propertyInfo.SetValue(targetType, service, null);
                try
                {
                    service.Initialize();
                }
                catch (Exception e)
                {
                    Debug.LogError(string.Format("[ServiceInitializator] Can't initialize service {0}\r\nException: {1}", propertyType, e));
                }
                while (!service.IsInitialized)
                {
                    yield return new WaitForEndOfFrame();
                }

                initializedTypes.Add(propertyType);
            }

            while (typesToInitialize.Count > 0)
            {
                var newDic = new Dictionary<PropertyInfo, ModuleAttribute>();
                foreach (var pair in typesToInitialize)
                {
                    if (pair.Value.Dependecies.Any(t => !initializedTypes.Contains(t)))
                    {
                        newDic.Add(pair.Key, pair.Value);
                    }
                    else
                    {
                        var service = InitializeService(pair.Key.PropertyType, pair.Value);

                        pair.Key.SetValue(targetType, service, null);
                        try
                        {
                            service.Initialize();
                        }
                        catch (Exception e)
                        {
                            Debug.LogError(string.Format("[ServiceInitializator] Can't initialize service {0}\r\nException: {1}", pair.Key.PropertyType, e));
                        }
                        while (!service.IsInitialized)
                        {
                            yield return new WaitForEndOfFrame();
                        }

                        initializedTypes.Add(pair.Key.PropertyType);
                    }
                }

                typesToInitialize = newDic;
            }
        }

        public static void DisposeServices(Type targetType)
        {
            var properties = targetType.GetProperties(BindingFlags.Static | BindingFlags.Public);

            foreach (var propertyInfo in properties)
            {
                var propertyType = propertyInfo.PropertyType;
                if (typeof(IModule).IsAssignableFrom(propertyType) == false)
                {
                    continue;
                }

                var service = propertyInfo.GetValue(targetType, null) as IModule;
                if (service != null)
                {
                    service.Dispose();
                }
            }
        }
    }
}
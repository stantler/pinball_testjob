using System;
using Helpers.Modules;
using UnityEngine;

[AddComponentMenu("Application/ApplicationManager")]
public class ApplicationManagerComponent : MonoBehaviour
{
    private static readonly Type[] InstanceTypes = { typeof(_), };

    public static ApplicationManagerComponent Instance;

    private void Start()
    {
        Instance = this;

        ModuleInitializator.Parent = transform;
        foreach (var type in InstanceTypes)
        {
            ModuleInitializator.InitializeServices(type, Instance);
        }
    }

    private void OnApplicationQuit()
    {
        foreach (var type in InstanceTypes)
        {
            ModuleInitializator.DisposeServices(type);
        }
    }
}

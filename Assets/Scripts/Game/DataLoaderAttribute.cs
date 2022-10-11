using System;
using System.Collections.Generic;
using UnityEngine;

[AttributeUsage(AttributeTargets.Class)]
public class DataLoaderAttribute : Attribute
{
    private string[] paths;
    private Dictionary<string, ScriptableObject> objects;

    public DataLoaderAttribute(params string[] paths)
    {
        this.paths = paths;
    }

    public ScriptableObject GetObject(string objectName)
    {
        return GetObjects().GetValueOrDefault(objectName);
    }

    private Dictionary<string, ScriptableObject> GetObjects()
    {
        if (objects == null) LoadObjects();
        return objects;
    }

    private void LoadObjects()
    {
        objects = new();
        foreach (string path in paths)
            foreach (ScriptableObject obj in Resources.LoadAll<ScriptableObject>(path))
                objects.Add(obj.name, obj);
    }
}

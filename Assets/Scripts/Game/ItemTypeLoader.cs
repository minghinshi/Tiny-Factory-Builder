using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class DataLoader<T> where T : ScriptableObject
{
    private readonly Dictionary<string, T> itemTypes = new();

    public DataLoader(string path)
    {
        LoadItemTypes(path);
    }

    public T GetItemType(string itemName)
    {
        if (itemName == null) return null;
        return itemTypes[itemName];
    }

    public List<T> GetAllItemTypes()
    {
        return itemTypes.Values.ToList();
    }

    private void LoadItemTypes(string path)
    {
        Resources.LoadAll<T>(path).ToList().ForEach(x => itemTypes.Add(x.name, x));
    }
}

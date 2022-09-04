using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ItemTypeLoader
{
    private readonly Dictionary<string, ItemType> itemTypes = new();

    public static ItemTypeLoader Create()
    {
        ItemTypeLoader loader = new();
        loader.LoadItemTypes();
        return loader;
    }

    public ItemType GetItemType(string itemName)
    {
        if (itemName == null) return null;
        return itemTypes[itemName];
    }

    public List<ItemType> GetAllItemTypes()
    {
        return itemTypes.Values.ToList();
    }

    private void LoadItemTypes()
    {
        Resources.LoadAll<ItemType>("Data").ToList().ForEach(x => itemTypes.Add(x.name, x));
    }
}

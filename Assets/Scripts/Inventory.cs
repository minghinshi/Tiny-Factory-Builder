using System.Collections.Generic;

public class Inventory
{
    private Dictionary<ItemType, int> storedItems = new Dictionary<ItemType, int>();

    public int GetItemCountOf(ItemType itemName)
    {
        if (!storedItems.ContainsKey(itemName))
            storedItems[itemName] = 0;
        return storedItems[itemName];
    }

    public void SetItemCountOf(ItemType itemName, int count)
    {
        storedItems[itemName] = count;
    }

    public void Store(ItemType itemName, int count)
    {
        SetItemCountOf(itemName, GetItemCountOf(itemName) + count);
    }

    public void StoreOne(ItemType itemName)
    {
        Store(itemName, 1);
    }

    public bool Remove(ItemType itemName, int count)
    {
        if (GetItemCountOf(itemName) < count) return false;
        SetItemCountOf(itemName, GetItemCountOf(itemName) - count);
        return true;
    }

    public bool RemoveOne(ItemType itemName)
    {
        return Remove(itemName, 1);
    }
}

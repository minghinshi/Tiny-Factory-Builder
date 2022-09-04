using System;
using UnityEngine;
using Newtonsoft.Json;

[Serializable]
public class ItemStack
{
    [SerializeField] [JsonProperty] private ItemType itemType;
    [SerializeField] [JsonProperty] private int count;

    public ItemStack(ItemType itemType, int count)
    {
        this.itemType = itemType;
        this.count = count;
    }

    public ItemType GetItemType()
    {
        return itemType;
    }

    public int GetCount()
    {
        return count;
    }

    public void Store(int amountToStore)
    {
        count += amountToStore;
    }

    public void Remove(int amountToRemove)
    {
        if (amountToRemove > count) throw new InvalidOperationException();
        count -= amountToRemove;
    }

    public ItemStack GetCopies(int multiplier)
    {
        return new ItemStack(itemType, count * multiplier);
    }

    public bool IsEmpty()
    {
        return count == 0;
    }
}
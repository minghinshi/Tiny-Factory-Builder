using Newtonsoft.Json;
using System;
using UnityEngine;

[Serializable]
public class ItemStack : ICountableItem
{
    [SerializeField, JsonProperty] private ItemType itemType;
    [SerializeField, JsonProperty] private int count;

    public delegate void CountChangedHandler();
    public event CountChangedHandler CountChanged;

    public ItemStack(ItemType itemType, int count)
    {
        this.itemType = itemType;
        this.count = count;
    }

    //Required for JSON serialization
    public ItemStack() { }

    public ItemStack GetItemStack()
    {
        return this;
    }

    public ItemType GetItemType()
    {
        return itemType;
    }

    public int GetCount()
    {
        return count;
    }

    public string GetCountAsString()
    {
        return count.ToString("N0");
    }

    public void Store(int amountToStore)
    {
        count += amountToStore;
        CountChanged?.Invoke();
    }

    public void Remove(int amountToRemove)
    {
        if (amountToRemove > count) 
            throw new InvalidOperationException();
        count -= amountToRemove;
        CountChanged?.Invoke();
    }

    public ICountableItem MultiplyBy(int multiplier)
    {
        return new ItemStack(itemType, count * multiplier);
    }

    public bool IsEmpty()
    {
        return count == 0;
    }
}
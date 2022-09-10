using Newtonsoft.Json;
using System;
using UnityEngine;

[Serializable]
public class ItemStack : IRecipeOutput
{
    [SerializeField, JsonProperty] private ItemType itemType;
    [SerializeField, JsonProperty] private int count;

    public ItemStack(ItemType itemType, int count)
    {
        this.itemType = itemType;
        this.count = count;
    }

    public ItemStack() : this(null, 0) { }

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

    public void Store(int amountToStore)
    {
        count += amountToStore;
    }

    public void Remove(int amountToRemove)
    {
        if (amountToRemove > count) throw new InvalidOperationException();
        count -= amountToRemove;
    }

    public IRecipeOutput MultiplyBy(int multiplier)
    {
        return new ItemStack(itemType, count * multiplier);
    }

    public bool IsEmpty()
    {
        return count == 0;
    }
}
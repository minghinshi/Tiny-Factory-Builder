using System;
using UnityEngine;

[Serializable]
public class ItemStack : IDisplayableAsItem
{
    [SerializeField] private ItemType itemType;
    [SerializeField] private uint count;

    public ItemStack(ItemType itemType, uint count)
    {
        this.itemType = itemType;
        this.count = count;
    }

    public ItemStack(ItemType itemType) : this(itemType, 0) { }

    public ItemType GetItemType()
    {
        return itemType;
    }

    public uint GetCount()
    {
        return count;
    }

    public void Store(uint amountToStore)
    {
        count += amountToStore;
    }

    public void Remove(uint amountToRemove)
    {
        if (amountToRemove > count)
            throw new InvalidOperationException();
        count -= amountToRemove;
    }

    public bool IsEmpty() {
        return count == 0;
    }
}
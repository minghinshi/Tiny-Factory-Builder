using System;
using UnityEngine;

[Serializable]
public class ItemStack : IDisplayableAsItem
{
    [SerializeField] private ItemType itemType;
    [SerializeField] private int count;

    public ItemStack(ItemType itemType, int count)
    {
        this.itemType = itemType;
        this.count = count;
    }

    public ItemStack(ItemType itemType) : this(itemType, 0) { }

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

    public bool IsEmpty()
    {
        return count == 0;
    }
}
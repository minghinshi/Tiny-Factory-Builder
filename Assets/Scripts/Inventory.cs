using System;
using System.Collections.Generic;

public class Inventory
{
    private List<ItemStack> itemStacks = new List<ItemStack>();

    public void Store(ItemType itemType, uint count)
    {

        ItemStack itemStack = GetItemStack(itemType) ?? AddItemStack(itemType);
        itemStack.Store(count);
    }

    public void StoreOne(ItemType itemType)
    {
        Store(itemType, 1);
    }

    public void Remove(ItemType itemType, uint count)
    {
        if (count > GetItemCount(itemType))
            throw new InvalidOperationException();
        ItemStack itemStack = GetItemStack(itemType);
        itemStack.Remove(count);
        if (itemStack.GetCount() == 0)
            itemStacks.Remove(itemStack);
    }

    public void RemoveOne(ItemType itemType)
    {
        Remove(itemType, 1);
    }

    public void RemoveCopyOf(ItemStack itemStack)
    {
        Remove(itemStack.GetItemType(), itemStack.GetCount());
    }

    public uint GetItemCount(ItemType itemType)
    {
        ItemStack itemStack = GetItemStack(itemType);
        return itemStack == null ? 0 : itemStack.GetCount();
    }

    private ItemStack AddItemStack(ItemType itemType)
    {
        ItemStack itemStack = new ItemStack(itemType);
        itemStacks.Add(itemStack);
        return itemStack;
    }

    private ItemStack GetItemStack(ItemType itemType)
    {
        return itemStacks.Find(x => x.GetItemType() == itemType);
    }
}
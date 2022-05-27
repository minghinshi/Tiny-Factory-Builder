using System;
using System.Collections.Generic;

public class Inventory
{
    private List<ItemStack> itemStacks;

    public delegate void InventoryUpdatedHandler();
    public event InventoryUpdatedHandler InventoryUpdated;

    public Inventory(params ItemStack[] itemStacks) {
        this.itemStacks = new List<ItemStack>(itemStacks);
    }

    public void Store(ItemType itemType, uint count)
    {
        ItemStack itemStack = GetItemStack(itemType) ?? AddItemStack(itemType);
        itemStack.Store(count);
        NotifyUpdate();
    }

    public void StoreCopyOf(ItemStack itemStack) {
        Store(itemStack.GetItemType(), itemStack.GetCount());
    }

    public void Remove(ItemType itemType, uint count)
    {
        if (count > GetItemCount(itemType))
            throw new InvalidOperationException();
        ItemStack itemStack = GetItemStack(itemType);
        itemStack.Remove(count);
        if (itemStack.GetCount() == 0)
            itemStacks.Remove(itemStack);
        NotifyUpdate();
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

    public bool HasItems() {
        return itemStacks.Count != 0;
    }

    public ItemType GetFirstItemType() {
        if (HasItems()) return itemStacks[0].GetItemType();
        else return null;
    }

    public ItemStack[] GetAllItemStacks() {
        return itemStacks.ToArray();
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

    private void NotifyUpdate() {
        InventoryUpdated?.Invoke();
    }
}
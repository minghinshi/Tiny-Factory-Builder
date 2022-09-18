using Newtonsoft.Json;
using System;
using System.Collections.Generic;

public class Inventory
{
    public static Inventory playerInventory = new(ScriptableObjectLoader.starterMachines.ConvertAll(x => new ItemStack(x, 1)).ToArray());

    [JsonProperty] private List<ItemStack> itemStacks;

    public delegate void InventoryUpdatedHandler();
    public event InventoryUpdatedHandler Updated;

    public delegate void OutOfStockHandler(ItemType itemType);
    public event OutOfStockHandler OutOfStock;

    public Inventory(params ItemStack[] itemStacks)
    {
        this.itemStacks = new List<ItemStack>(itemStacks);
    }

    public void Store(ItemType itemType, int count)
    {
        ItemStack itemStack = GetItemStack(itemType) ?? AddItemStack(itemType);
        itemStack.Store(count);
        NotifyUpdate();
    }

    public void StoreCopyOf(ItemStack itemStack)
    {
        StoreCopiesOf(itemStack, 1);
    }

    public void StoreCopiesOf(ItemStack itemStack, int numberOfCopies)
    {
        Store(itemStack.GetItemType(), itemStack.GetCount() * numberOfCopies);
    }

    public void Remove(ItemType itemType, int count)
    {
        if (count > GetItemCount(itemType)) throw new InvalidOperationException();
        ItemStack itemStack = GetItemStack(itemType);
        itemStack.Remove(count);
        if (itemStack.GetCount() == 0) RemoveItemStack(itemStack);
        NotifyUpdate();
    }

    public void RemoveCopyOf(ItemStack itemStack)
    {
        RemoveCopiesOf(itemStack, 1);
    }

    public void RemoveCopiesOf(ItemStack itemStack, int numberOfCopies)
    {
        Remove(itemStack.GetItemType(), itemStack.GetCount() * numberOfCopies);
    }

    public void Empty()
    {
        itemStacks.Clear();
        NotifyUpdate();
    }

    public int GetItemCount(ItemType itemType)
    {
        ItemStack itemStack = GetItemStack(itemType);
        return itemStack == null ? 0 : itemStack.GetCount();
    }

    public bool HasItems()
    {
        return itemStacks.Count != 0;
    }

    public bool Contains(ItemType itemType)
    {
        return GetItemStack(itemType) != null;
    }

    public bool Contains(ItemStack itemStack)
    {
        return GetItemCount(itemStack.GetItemType()) >= itemStack.GetCount();
    }

    public ItemType GetFirstItemType()
    {
        if (HasItems()) return itemStacks[0].GetItemType();
        else return null;
    }

    public List<ItemStack> GetAllItemStacks()
    {
        return itemStacks;
    }

    public void TransferTo(Inventory inventory)
    {
        itemStacks.ForEach(x => inventory.StoreCopyOf(x));
        Empty();
    }

    private ItemStack AddItemStack(ItemType itemType)
    {
        ItemStack itemStack = new(itemType, 0);
        itemStacks.Add(itemStack);
        return itemStack;
    }

    private ItemStack GetItemStack(ItemType itemType)
    {
        return itemStacks.Find(x => x.GetItemType() == itemType);
    }

    private void RemoveItemStack(ItemStack itemStack)
    {
        itemStacks.Remove(itemStack);
        OutOfStock?.Invoke(itemStack.GetItemType());
    }

    private void NotifyUpdate()
    {
        Updated?.Invoke();
    }
}
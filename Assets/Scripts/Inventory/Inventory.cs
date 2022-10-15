using Newtonsoft.Json;
using System.Collections.Generic;

public class Inventory
{
    public static Inventory playerInventory;

    [JsonProperty] private List<ItemStack> itemStacks;

    public delegate void InventoryChangedHandler();
    public event InventoryChangedHandler Changed;

    public delegate void ItemTypesChangedHandler(ItemType itemType);
    public event ItemTypesChangedHandler ItemAdded;
    public event ItemTypesChangedHandler ItemRemoved;

    public Inventory(params ItemStack[] itemStacks)
    {
        this.itemStacks = new List<ItemStack>(itemStacks);
    }

    public void StoreSingle(ItemType itemType)
    {
        Store(itemType, 1);
    }

    public void RemoveSingle(ItemType itemType)
    {
        Remove(itemType, 1);
    }

    public void StoreStack(ItemStack itemStack)
    {
        Store(itemStack.GetItemType(), itemStack.GetCount());
    }

    public void RemoveStack(ItemStack itemStack)
    {
        Remove(itemStack.GetItemType(), itemStack.GetCount());
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
        itemStacks.ForEach(x => inventory.StoreStack(x));
        Empty();
    }

    protected virtual void Store(ItemType itemType, int count)
    {
        ItemStack itemStack = GetItemStack(itemType) ?? AddItemStack(itemType);
        itemStack.Store(count);
        NotifyUpdate();
    }

    protected virtual void Remove(ItemType itemType, int count)
    {
        ItemStack itemStack = GetItemStack(itemType);
        if (itemStack.GetCount() == count) RemoveItemStack(itemStack);
        else itemStack.Remove(count);
        NotifyUpdate();
    }

    private void Empty()
    {
        itemStacks.Clear();
        NotifyUpdate();
    }

    private ItemStack AddItemStack(ItemType itemType)
    {
        ItemStack itemStack = new(itemType, 0);
        itemStacks.Add(itemStack);
        ItemAdded?.Invoke(itemType);
        return itemStack;
    }

    private ItemStack GetItemStack(ItemType itemType)
    {
        return itemStacks.Find(x => x.GetItemType() == itemType);
    }

    private void RemoveItemStack(ItemStack itemStack)
    {
        itemStacks.Remove(itemStack);
        ItemRemoved?.Invoke(itemStack.GetItemType());
    }

    private void NotifyUpdate()
    {
        Changed?.Invoke();
    }
}
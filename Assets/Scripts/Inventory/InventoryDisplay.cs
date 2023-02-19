using System;
using System.Collections.Generic;
using UnityEngine;

public class InventoryDisplay : MonoBehaviour
{
    private Inventory targetInventory;
    private Func<ItemStack, ItemLabel> buildFunc;
    private readonly Dictionary<ItemType, ItemLabel> itemLabels = new();

    public void SetTargetInventory(Inventory inventory)
    {
        DisconnectFromInventory();
        ConnectToInventory(inventory);
    }

    public void SetBuildFunc(Func<ItemStack, ItemLabel> buildFunc)
    {
        this.buildFunc = buildFunc;
    }

    private void OnDestroy()
    {
        DisconnectFromInventory();
    }

    private void ConnectToInventory(Inventory inventory)
    {
        targetInventory = inventory;
        inventory.GetAllItemStacks().ForEach(x => AddItemType(x.GetItemType()));
        inventory.ItemAdded += AddItemType;
        inventory.ItemRemoved += RemoveItemType;
        inventory.Cleared += ClearLabels;
    }

    private void DisconnectFromInventory()
    {
        if (targetInventory == null) return;
        targetInventory.ItemAdded -= AddItemType;
        targetInventory.ItemRemoved -= RemoveItemType;
        targetInventory.Cleared -= ClearLabels;
        targetInventory = null;
        ClearLabels();
    }

    private ItemLabel BuildLabel(ItemType itemType)
    {
        ItemStack itemStack = targetInventory.GetItemStack(itemType);
        ItemLabel label = buildFunc.Invoke(itemStack);
        label.Counter.BindToItemStack(itemStack);
        label.transform.SetParent(transform, false);
        return label;
    }

    private void AddItemType(ItemType itemType)
    {
        ItemLabel label = BuildLabel(itemType);
        itemLabels.Add(itemType, label);
    }

    private void RemoveItemType(ItemType itemType)
    {
        itemLabels.Remove(itemType, out ItemLabel label);
        print("Removed " + itemType.ToString());
        Destroy(label.gameObject);
    }

    private void ClearLabels()
    {
        itemLabels.Clear();
        foreach (Transform child in transform) Destroy(child.gameObject);
    }
}
